using System;
using System.Collections.Generic;
using System.Linq;

namespace PokeBrowser.Data
{
    public enum PersonalityParameter
    {
        None,
        Atack,
        Defence,
        SpAtack,
        SpDefence,
        Speed,
    }

    /// <summary>
    /// 性格データ
    /// </summary>
    public class PersonalityData
    {
        public string Name { get; private set; }

        public PersonalityParameter Up { get; private set; }

        public PersonalityParameter Down { get; private set; }

        public PersonalityData(string name, PersonalityParameter up, PersonalityParameter down)
        {
            Name = name;
            Up = up;
            Down = down;
        }
    }
}
