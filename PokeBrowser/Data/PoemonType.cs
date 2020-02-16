using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeBrowser.Data
{
    public class TypeData
    {
        public string Name { get; set; } = string.Empty;
        public string[] SupperEffective { get; set; } = Array.Empty<string>();
        public string[] BadEffective { get; set; } = Array.Empty<string>();
        public string[] NoEffective { get; set; } = Array.Empty<string>();
    }

    public class PokemonType
    {
        public string Name { get; }

        public PokemonType[] SupperEffective { get; private set; }

        public PokemonType[] BadEffective { get; private set; }

        public PokemonType[] NoEffective { get; private set; }

        private readonly TypeData _serializeData = null;

        public PokemonType(TypeData data)
        {
            _serializeData = data;
            Name = data.Name;
        }

        public void SetEffectiveSource(IDictionary<string,PokemonType> source)
        {
            SupperEffective = _serializeData.SupperEffective.Select(x => source[x]).ToArray();
            BadEffective = _serializeData.BadEffective.Select(x => source[x]).ToArray();
            NoEffective = _serializeData.NoEffective.Select(x => source[x]).ToArray();
        }
    }
}
