using PokeBrowser.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PokeBrowser.ViewModels
{
    public class PokemonVm : Livet.ViewModel
    {
        public string Name { get; }
        public string Type1 { get; }
        public string Type2 { get; }
        public int Hp { get; }
        public int Atack { get; }
        public int Defence { get; }
        public int SpAtack {get;}
        public int SpDefence { get; }
        public int Speed { get; }
        public int Total { get; }
        public string Ability1 { get; }
        public string Ability2 { get; }
        public string Ability3 { get; }
        public string Group1 { get; }
        public string Group2 { get; }
        public string Moves { get; }
        public string ImageSource { get; }

        public PokemonData Model { get; }

        public PokemonVm(PokemonData pokemonData )
        {
            Model = pokemonData;

            Name = pokemonData.Name;

            if(string.IsNullOrEmpty(pokemonData.Form) is false )
            {
                if (Name != pokemonData.Form)
                {
                    var form = pokemonData.Form;
                    if(form.Contains("のすがた"))
                        form = form.Substring(0,form.IndexOf("のすがた"));
                    if (form.Contains("フォルム"))
                        form = form.Substring(0, form.IndexOf("フォルム"));

                    Name += $"({form})";
                }
            }

            Type1 = pokemonData.Type1;
            Type2 = pokemonData.Type2 ?? string.Empty;
            Hp = pokemonData.Hp;
            Atack = pokemonData.Attack;
            Defence = pokemonData.Defense;
            SpAtack = pokemonData.SpecialAttack;
            SpDefence = pokemonData.SpecialDefense;
            Speed = pokemonData.Speed;
            Total = Hp + Atack + Defence + SpAtack + SpDefence + Speed;

            Group1 = pokemonData.Group1;
            Group2 = pokemonData.Group2;

            Ability1 = pokemonData.Ability1;
            Ability2 = pokemonData.Ability2 ?? string.Empty;
            Ability3 = pokemonData.Ability3 ?? string.Empty;

            ImageSource = Path.Combine(FilePath.PokemonIconDirectoryPath, $"icon{pokemonData.Id}.gif");
        }
    }
}
