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

    public class PersonalityRoot
    {
        public PersonalityData[] Items { get; set; }

        public static PersonalityRoot ConvertFromYaml(IList<object> yamlData)
        {
            return new PersonalityRoot()
            {
                Items = yamlData
                .OfType<Dictionary<object, object>>()
                .Select(x =>
                {
                    var name = x[nameof(PersonalityData.Name)] as string;
                    var up = (PersonalityParameter)Enum.Parse(typeof(PersonalityParameter), x[nameof(PersonalityData.Up)] as string);
                    var down = (PersonalityParameter)Enum.Parse(typeof(PersonalityParameter), x[nameof(PersonalityData.Down)] as string);

                    return new PersonalityData(name, up, down); ;
                }).ToArray(),
            };
        }
    }
}
