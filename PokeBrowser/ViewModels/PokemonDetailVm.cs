using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;
using Livet.Messaging;
using PokeBrowser.Data;
using PokeBrowser.Foundation;
using PokeBrowser.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace PokeBrowser.ViewModels
{
    public class PokemonFormVm : Livet.NotificationObject
    {
        public string DisplayName { get; }
        
        public PokemonData Model { get; }

        public PokemonFormVm(PokemonData pokemonData)
        {
            Model = pokemonData;
            DisplayName = pokemonData.Form ?? pokemonData.Name;
        }
    }
    
    public class PokemonDetailVm : PokemonVm
    {
        /// <summary>
        /// 性格
        /// </summary>
        public IReactiveProperty<string> Personality { get; }
        
        /// <summary>
        /// レベル
        /// </summary>
        public IReactiveProperty<int> Level { get; }
        
        /// <summary>
        /// 検索ボックスからポケモンを変更する
        /// </summary>
        public IReactiveProperty<string> NameWithSearchBox { get; } 
        
        /// <summary>
        /// クリップボードにパラメータをコピーするための表示用値
        /// </summary>
        public IReactiveProperty<string> ClipBoardParameter { get; }

        public IReactiveProperty<DamageCalcVm> DamageCalcVm { get; }
        
        /// <summary>
        /// 努力値
        /// </summary>
        public ParameterVm Ev { get; }

        /// <summary>
        /// 個体値
        /// </summary>
        public ParameterVm Iv { get; } 
        
        /// <summary>
        /// 計算済み実数値
        /// </summary>
        public ParameterVm Param { get; }
        
        /// <summary>
        /// ステータス計算機
        /// </summary>
        private PokemonParameterCalculator ParameterCalculator { get; } = new PokemonParameterCalculator();

        /// <summary>
        /// 個体値の合計と残りを表示するための値
        /// </summary>
        public IReactiveProperty<string> TotalEv { get; }
        
        /// <summary>
        /// 個体値を設定するコマンド
        /// </summary>
        public ICommand SetIvCommand { get; }
        
        /// <summary>
        /// 努力値を設定するためのコマンド
        /// </summary>
        public ICommand SetEvCommand { get; }
        
        public ICommand ChangePokemonCommand { get; }
        
        public ICommand ShowDetailCommand { get; }
        
        
        public ObservableCollection<PokemonFormVm> Forms { get; } = new ObservableCollection<PokemonFormVm>();

        public PokemonDetailVm()
        {
            // デザイナビューから利用
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="pokemonData"></param>
        public PokemonDetailVm(PokemonData pokemonData) : base(pokemonData)
        {
            DamageCalcVm = new ReactivePropertySlim<DamageCalcVm>();
            UpdateFormSource();
            ParameterCalculator.SetPokemon(pokemonData.Name , pokemonData.Form);
            Ev = new ParameterVm(new ParameterData<int>());
            Iv = new ParameterVm(new ParameterData<int>(31,31,31,31,31,31));
            Param = new ParameterVm(new ParameterData<int>());
            Personality = new ReactivePropertySlim<string>(string.Empty);
            Level = new ReactiveProperty<int>(50).AddTo(CompositeDisposable);
            ClipBoardParameter = new ReactiveProperty<string>(string.Empty);
            
            SetIvCommand = new DelegateCommand<string>(x=>Iv.Model.Set(x));
            SetEvCommand = new DelegateCommand<string>(x=>Ev.Model.Set(x));
            ChangePokemonCommand = new DelegateCommand<PokemonData>(x =>
            {
                ChangePokemon(x.Name,x.Form);
            });
            ShowDetailCommand = new DelegateCommand(() => Messenger.Raise(new TransitionMessage(new PokemonDetailVm(Model), "ShowDetail")));
            
            NameWithSearchBox = new ReactiveProperty<string>(string.Empty).AddTo(CompositeDisposable);
            NameWithSearchBox.Subscribe(_ => ChangePokemon(_, null)).AddTo(CompositeDisposable);

             Observable.Merge(
                    Ev.Model.PropertyChangedAsObservable().ToUnit(),
                    Iv.Model.PropertyChangedAsObservable().ToUnit(),
                    Personality.ToUnit(),
                    Level.ToUnit())
                .StartWith()
                .Throttle(TimeSpan.FromMilliseconds(50))
                .ObserveOnDispatcher()
                .Do(_=>UpdateCalculator())
                .Do(_ => UpdateFromParameter())
                .Do(_ => GenerateClipBoardText())
                .Subscribe()
                .AddTo(CompositeDisposable);

             Param.Model.PropertyChangedAsObservable()
                 .StartWith()
                 .Throttle(TimeSpan.FromMilliseconds(50))
                 .ObserveOnDispatcher()
                 .Do(_ => UpdateFromParameter())
                 .Do(_ => GenerateClipBoardText())
                 .Subscribe()
                 .AddTo(CompositeDisposable);
            
            // 努力値合計と残努力値の適用
            TotalEv = Ev.Model
                .PropertyChangedAsObservable()
                .StartWith()
                .Select(x=>Ev.Model.ToArray().Sum())
                .Select(x=> $"計{x}残{510 - x}")
                .ToReactiveProperty()
                .AddTo(CompositeDisposable);
        }

        /// <summary>
        /// ポケモンを変更する
        /// </summary>
        /// <param name="name"></param>
        /// <param name="form"></param>
        private void ChangePokemon(string name, string form)
        {
            if (DataBaseService.DataBase.AnyPokemon(name, form) is false)
                return;
            SetModel(DataBaseService.DataBase.FindPokemon(name, form));
            UpdateFormSource();

            RaisePropertiesChange();
            ParameterCalculator.SetPokemon(Model.Name , Model.Form);

            UpdateCalculator();
            UpdateFromParameter();
            GenerateClipBoardText();
        }

        private void UpdateFormSource()
        {
            var forms = DataBaseService.DataBase.GetForms(Model.Id);
            Forms.Clear();
            if (forms.Any())
            {
                var origin = DataBaseService.DataBase.FindPokemon(Model.Id);
                Forms.Add(new PokemonFormVm(origin));
                foreach (var f in forms)
                    Forms.Add(new PokemonFormVm(f));
            }
            DamageCalcVm.Value = new DamageCalcVm(Model);
        }

        private void UpdateFromParameter()
        {
            //　実数値から努力値を逆算
            var param = ParameterCalculator.CalcEv(Level.Value,Param.Model);
            Ev.Model.CopyFrom(ref param);
        }
 
        /// <summary>
        /// 個体値・努力値・性格からパラメータを計算
        /// </summary>
        private void UpdateCalculator()
        {
            // 計算機にパラメータを設定
            ParameterCalculator.SetEv(Ev.Model);
            ParameterCalculator.SetIv(Iv.Model);            
            ParameterCalculator.SetPersonality(Personality.Value);

            // 個体値/努力値からパラメータ計算
            {
                var param = ParameterCalculator.Calc(Level.Value);
                Param.Model.CopyFrom(ref param);
            }
        }

        private void GenerateClipBoardText()
        {
            
            // クリップボード用のテキストを作成
            {
                var builder = new StringBuilder();
                builder.Append($"{Name} {Personality.Value} ");

                foreach (var i in Enumerable.Range(0,6))
                {
                    builder.Append(Param.Model.GetByIndex(i));
                
                    if(Ev.Model.GetByIndex(i) != 0)
                        builder.Append($"({Ev.Model.GetByIndex(i)})");
                    if(i != 5)
                        builder.Append('-');
                }

                ClipBoardParameter.Value = builder.ToString();                
            }
        }
    }
}