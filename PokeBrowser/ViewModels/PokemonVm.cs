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
        public string Name { get; }
        public string Type1 { get; }
        public string Type2 { get; }
        public int Hp { get; }
        public int Attack { get; }
        public int Defense { get; }
        public int SpAttack { get; }
        public int SpDefense { get; }
        public int Speed { get; }
        public int Total { get; }
        public string Ability1 { get; }
        public string Ability2 { get; }
        public string Ability3 { get; }
        public string Group1 { get; }
        public string Group2 { get; }
        public string[] Moves { get; }
        public string ImageSource { get; protected set; }

        public PokemonData Model { get; }

        public PokemonVm()
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
    }
}
