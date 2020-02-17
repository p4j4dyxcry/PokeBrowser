using System;
using System.ComponentModel;
using PokeBrowser.Data;
using System.IO;
using System.Linq;
using System.Windows;
using PokeBrowser.Models;

namespace PokeBrowser.ViewModels
{
    public class PokemonVm : Livet.ViewModel
    {
        public string Name { get; private set; }
        public string Type1 { get; private set;}
        public string Type2 { get;private set;}
        public int Hp { get; private set;}
        public int Attack { get; private set;}
        public int Defense { get; private set;}
        public int SpAttack { get; private set;}
        public int SpDefense { get; private set;}
        public int Speed { get; private set;}
        public int Total { get; private set;}
        public string Ability1 { get; private set;}
        public string Ability2 { get; private set;}
        public string Ability3 { get; private set; }
        public string Group1 { get; private set;}
        public string Group2 { get; private set;}
        public string[] Moves { get; private set;}
        public string ImageSource { get; private set; }

        public PokemonData Model { get; private set; }

        protected PokemonVm()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()) is false)
            {
                throw new InvalidOperationException("デザイナーモード以外利用はできません。");
            }
            // Designer
            Name = "テストポケモン";
            Type1 = "フェアリー";
            Type2 = "でんき";

            Hp = 250;
            Attack = 135;
            Defense = 50;
            SpAttack = 100;
            SpDefense = 10;
            Speed = 90;
            Total = Hp + Attack + Defense + SpAttack + SpDefense + Speed;

            Ability1 = "ようりょくそ";
            Ability2 = "もうか";
            Ability3 = "プレッシャー";

            Group1 = "妖精";
            Group2 = "水中2";

            Moves = new[]
            {
                "たいあたり",
                "みがわり",
                "きあいだめ",
                "ダークホール",
                "あなをほる",
                "ソーラービーム",
                "うたう",
                "サイコキネシス",
                "ちょうはつ",
                "ステルスロック",
                "キノコのほうし",
                "はかいこうせん",
                "でんきショック",
                "サイドチェンジ",
                "なげつける",
                "はさむ",
                "こらえる",
                "かえんほうしゃ",
                "まもる",
                "みきり",
                "パワーシェア",
                "ダイビング",
                "みやぶる",
                "あおいほのお",
                "そらをとぶ",
                "いわくだき",
                "ゆびをふる",
                "たくわえる",
                "にらみつける",
                "ひのこ",
                "だいばくはつ",
                "いあいぎり",
                "かいりき",
                "なみのり",
                "ソーラーブレード",
                "でんじほう"
            };
            ImageSource = "D:/work/temp.gif";

        }

        public PokemonVm(PokemonData pokemonData)
        {
            SetModel(pokemonData);
        }

        protected void SetModel(PokemonData pokemonData)
        {
            Model = pokemonData;

            Name = pokemonData.Name;

            if (string.IsNullOrEmpty(pokemonData.Form) is false)
            {
                if (Name != pokemonData.Form)
                {
                    var form = pokemonData.Form;
                    if (form.Contains("のすがた"))
                        form = form.Substring(0, form.IndexOf("のすがた", StringComparison.Ordinal));
                    if (form.Contains("フォルム"))
                        form = form.Substring(0, form.IndexOf("フォルム", StringComparison.Ordinal));

                    Name += $"({form})";
                }
            }

            Type1 = pokemonData.Type1;
            Type2 = pokemonData.Type2 ?? string.Empty;
            Hp = pokemonData.Hp;
            Attack = pokemonData.Attack;
            Defense = pokemonData.Defense;
            SpAttack = pokemonData.SpecialAttack;
            SpDefense = pokemonData.SpecialDefense;
            Speed = pokemonData.Speed;
            Total = Hp + Attack + Defense + SpAttack + SpDefense + Speed;

            Group1 = pokemonData.Group1;
            Group2 = pokemonData.Group2;

            Ability1 = pokemonData.Ability1;
            Ability2 = pokemonData.Ability2 ?? string.Empty;
            Ability3 = pokemonData.Ability3 ?? string.Empty;

            Moves = pokemonData.Moves.ToArray();

            ImageSource = Path.Combine(FilePath.PokemonIconDirectoryPath, $"icon{pokemonData.Id}.gif");
        }

        protected void RaisePropertiesChange()
        {
            RaisePropertyChanged(nameof(Name));
            RaisePropertyChanged(nameof(Hp));
            RaisePropertyChanged(nameof(Attack));
            RaisePropertyChanged(nameof(Defense));
            RaisePropertyChanged(nameof(SpAttack));
            RaisePropertyChanged(nameof(SpDefense));
            RaisePropertyChanged(nameof(Speed));
            RaisePropertyChanged(nameof(Total));
            RaisePropertyChanged(nameof(Type1));
            RaisePropertyChanged(nameof(Type2));
            RaisePropertyChanged(nameof(Ability1));
            RaisePropertyChanged(nameof(Ability2));
            RaisePropertyChanged(nameof(Ability3));
            RaisePropertyChanged(nameof(Moves));
            RaisePropertyChanged(nameof(ImageSource));
        }
    }
}
