using System;
using System.Collections.Generic;
using System.Linq;

namespace PokeBrowser.Data
{
    public class DataBase
    {
        /// <summary>
        /// 特性データ
        /// </summary>
        public AbilityData[] Abilities { get; private set; }

        /// <summary>
        /// タイプデータ
        /// </summary>
        public PokemonType[] Types { get; private set; }
        
        /// <summary>
        /// 性格データ
        /// </summary>
        public PersonalityData[] Personalities { get; private set; }

        /// <summary>
        /// ポケモンデータ
        /// </summary>
        public PokemonData[] Pokemons { get; private set; }

        /// <summary>
        /// フォルムデータ
        /// </summary>
        public PokemonData[] Forms { get; private set; }

        /// <summary>
        /// 技データ
        /// </summary>
        public MoveData[] Moves { get; private set; }

        private IDictionary<int, PokemonData> _pokemonDictionary = null;
        private IDictionary<int, PokemonData[]> _formDictionary = null;
        private IDictionary<string, PokemonType> _typeDictionary = null;
        private IDictionary<string, AbilityData>     _abliDictionary = null;
        private IDictionary<string, PersonalityData> _persDictionary = null;
        private IDictionary<string, MoveData> _moveDictionary = null;

        public PokemonType FindType(string name) => _typeDictionary[name];
        public AbilityData FindAbility(string name) => _abliDictionary[name];
        public PersonalityData FindPersonality(string name) => _persDictionary[name];
        public PokemonData FindPokemon(string name , string form = null) => _pokemonDictionary[pokemon_hash(name,form)];
        
        public bool AnyPersonality(string name) => _persDictionary.ContainsKey(name);
        public bool AnyPokemon(string name,string form = null) => _pokemonDictionary.ContainsKey(pokemon_hash(name,form));

        public static DataBase Build(IEnumerable<AbilityData> abilities,
                                     IEnumerable<TypeData> types,
                                     IEnumerable<PersonalityData> personalities,
                                     IEnumerable<PokemonData> pokemonData,
                                     IEnumerable<MoveData> moves,
                                     IEnumerable<PokemonData> forms)
        {
            var db = new DataBase();
            db.Abilities = abilities.ToArray();
            db.Personalities = personalities.ToArray();
            db.Moves = moves.ToArray();
            db.Forms = forms.ToArray();
            db.Pokemons = pokemonData.ToArray();

            db.Types = types.Select(x => new PokemonType(x)).ToArray();
            db._typeDictionary = db.Types.ToDictionary(x=>x.Name,x=>x);
            db._abliDictionary = db.Abilities.ToDictionary(x => x.Name, x => x);
            db._persDictionary = db.Personalities.ToDictionary(x => x.Name, x => x);
            db._moveDictionary = db.Moves.ToDictionary(x => x.Name, x => x);
            db._pokemonDictionary = db.Pokemons.Concat(db.Forms).ToDictionary(x => pokemon_hash(x.Name, x.Form), x => x);
            db._formDictionary = db.Forms.GroupBy(x => x.Id).ToDictionary(x=>x.Key,x=>x.ToArray());

            foreach (var type in db.Types)
            {
                type.SetEffectiveSource(db._typeDictionary);
            }



            return db;
        }

        private static int pokemon_hash(string name, string form)
        {
            if (string.IsNullOrEmpty(form))
                return name.GetHashCode();

            if (name == form)
                return form.GetHashCode();

            return name.GetHashCode() ^ form.GetHashCode();
        }
    }

    public static class DataBaseService
    {
        public static DataBase DataBase { get; private set; }


        public static IEnumerable<string> Personalities => DataBase.Personalities.Select(x => x.Name);

        public static IEnumerable<string> Moves => DataBase.Moves.Select(x => x.Name);
        
        public static IEnumerable<string> Pokemons => DataBase.Pokemons.Select(x => x.Name);

        public static void Initialize(DataBase dataBase)
        {
            DataBase = dataBase;
        }
    }
}
