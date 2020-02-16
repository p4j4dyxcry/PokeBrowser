using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PokeBrowser.Data
{
    public class PokemonData
    {
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Form { get; set; }

        public int Id { get; set; }
        public int? GalarID { get; set; }
        public int? AlolaID { get; set; }
        public int? AlolaID2 { get; set; }

        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }

        public string Group1 { get; set; }
        public string Group2 { get; set; }

        public string Weight { get; set; }

        public string Ability1 { get; set; }
        public string Ability2 { get; set; }
        public string Ability3 { get; set; }

        public string[] Moves { get; set; }
        public string[] Obsoletes { get; set; }
    }
}