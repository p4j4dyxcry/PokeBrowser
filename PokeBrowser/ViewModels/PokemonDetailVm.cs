using System;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;
using PokeBrowser.Data;
using PokeBrowser.Foundation;
using PokeBrowser.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace PokeBrowser.ViewModels
{
    public class PokemonDetailVm : PokemonVm
    {
        /// <summary>
        /// 性格
        /// </summary>
        public IReactiveProperty<string> Personality { get; }
        
        public IReactiveProperty<string> Change { get; } 
        
        public IReactiveProperty<string> ClipBoardParameter { get; }

        /// <summary>
        /// 努力値
        /// </summary>
        public ParameterVm Ev { get; }

        /// <summary>
        /// 個体値
        /// </summary>
        public ParameterVm Iv { get; } 
        
        public ParameterVm Param { get; }
        
        private PokemonParameterCalculator ParameterCalculator { get; } = new PokemonParameterCalculator();

        public IReactiveProperty<string> TotalEv { get; }
        
        public ICommand SetIvCommand { get; }
        public ICommand SetEvCommand { get; }

        public PokemonDetailVm()
        {
            Ev = new ParameterVm(new ParameterData<int>());
            Iv = new ParameterVm(new ParameterData<int>(31, 31, 31, 31, 31, 31));
            Personality = new ReactivePropertySlim<string>(string.Empty);
        }

        public PokemonDetailVm(PokemonData pokemonData) : base(pokemonData)
        {
            ParameterCalculator.SetPokemon(pokemonData.Name , pokemonData.Form);
            Ev = new ParameterVm(new ParameterData<int>());
            Iv = new ParameterVm(new ParameterData<int>(31,31,31,31,31,31));
            Param = new ParameterVm(new ParameterData<int>());
            Personality = new ReactivePropertySlim<string>(string.Empty);
            ClipBoardParameter = new ReactiveProperty<string>(string.Empty);
            
            SetIvCommand = new DelegateCommand<string>(x=>Iv.Model.Set(x));
            SetEvCommand = new DelegateCommand<string>(x=>Ev.Model.Set(x));
            
            Change = new ReactiveProperty<string>(string.Empty).AddTo(CompositeDisposable);
            Change.Subscribe(_ => ChangePokemon(_, null)).AddTo(CompositeDisposable);

             Observable.Merge(
                    Ev.Model.PropertyChangedAsObservable().ToUnit(),
                    Iv.Model.PropertyChangedAsObservable().ToUnit(),
                    Param.Model.PropertyChangedAsObservable().ToUnit(),
                    Personality.ToUnit())
                .StartWith()
                .Throttle(TimeSpan.FromMilliseconds(50))
                .ObserveOnDispatcher()
                .Do(_=>UpdateCalculator())
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

        void ChangePokemon(string name, string form)
        {
            if (DataBaseService.DataBase.AnyPokemon(name, form) is false)
                return;
            SetModel(DataBaseService.DataBase.FindPokemon(name, form));
            RaisePropertiesChange();
            ParameterCalculator.SetPokemon(Model.Name , Model.Form);
            UpdateCalculator();
        }
 
        /// <summary>
        /// 個体値・努力値・性格からパラメータを計算
        /// </summary>
        private void UpdateCalculator()
        {
            ParameterCalculator.SetEv(Ev.Model);
            ParameterCalculator.SetIv(Iv.Model);            
            ParameterCalculator.SetPersonality(Personality.Value);

            // パラメータ計算
            {
                var param = ParameterCalculator.Calc(50);
                Param.Model.CopyFrom(ref param);
            }

            //　実数値から個体値を逆算
            {
                var param = ParameterCalculator.CalcEv(50,Param.Model);
                Ev.Model.CopyFrom(ref param);
            }

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