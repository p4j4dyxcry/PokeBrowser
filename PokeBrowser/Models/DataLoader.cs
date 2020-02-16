using System;
using System.IO;
using PokeBrowser.Data;
using PokeBrowser.Foundation;

namespace PokeBrowser.Models
{
    public static class DataLoader
    {
        /// <summary>
        /// 性格一覧をyamlに出力する
        /// </summary>
        public static void SavePersonalityToJson()
        {
            PersonalityData[] data = new[]
            {
                new PersonalityData("指定なし",  PersonalityParameter.None,PersonalityParameter.None),
                new PersonalityData("いじっぱり",PersonalityParameter.Atack,PersonalityParameter.SpAtack),
                new PersonalityData("ひかえめ",  PersonalityParameter.SpAtack,PersonalityParameter.Atack),
                new PersonalityData("ようき",    PersonalityParameter.Speed,PersonalityParameter.SpAtack),
                new PersonalityData("おくびょう",PersonalityParameter.Speed,PersonalityParameter.Atack),
                new PersonalityData("ずぶとい",  PersonalityParameter.Defence,PersonalityParameter.Atack),
                new PersonalityData("わんぱく",  PersonalityParameter.Defence,PersonalityParameter.SpAtack),
                new PersonalityData("おだやか",  PersonalityParameter.SpDefence,PersonalityParameter.Atack),
                new PersonalityData("しんちょう",PersonalityParameter.SpDefence,PersonalityParameter.SpAtack),
                new PersonalityData("のんき",    PersonalityParameter.Defence,PersonalityParameter.Speed),
                new PersonalityData("なまいき",  PersonalityParameter.SpDefence,PersonalityParameter.Speed),
                new PersonalityData("ゆうかん",  PersonalityParameter.Atack,PersonalityParameter.Speed),
                new PersonalityData("れいせい",  PersonalityParameter.SpAtack,PersonalityParameter.Speed),
                new PersonalityData("せっかち",  PersonalityParameter.Speed,PersonalityParameter.Defence),
                new PersonalityData("むじゃき",  PersonalityParameter.Speed,PersonalityParameter.SpDefence),
                new PersonalityData("さみしがり",PersonalityParameter.Atack,PersonalityParameter.Defence),
                new PersonalityData("やんちゃ",  PersonalityParameter.Atack,PersonalityParameter.SpDefence),
                new PersonalityData("おっとり",  PersonalityParameter.SpAtack,PersonalityParameter.Defence),
                new PersonalityData("うっかりや",PersonalityParameter.SpAtack,PersonalityParameter.SpDefence),
                new PersonalityData("のうてんき",PersonalityParameter.Defence,PersonalityParameter.SpDefence),
                new PersonalityData("おとなしい",PersonalityParameter.SpDefence,PersonalityParameter.Defence),
                new PersonalityData("まじめ",    PersonalityParameter.None,PersonalityParameter.None),
            };

            data.SerializeToFile(FilePath.PersonalityDataPath);
        }


        /// <summary>
        /// タイプ情報をyamlに出力する
        /// </summary>
        public static void SaveTypeToJson()
        {
            TypeData[] data = new[]
            {
                new TypeData()
                {
                    Name = "ノーマル",
                    BadEffective = new []{"いわ","はがね"},
                    NoEffective = new []{"ゴースト"},
                },

                new TypeData()
                {
                    Name = "ほのお",
                    SupperEffective = new[]{"くさ","こおり","むし","はがね"},
                    BadEffective = new []{ "ほのお", "みず" , "いわ" , "ドラゴン"},
                },

                new TypeData()
                {
                    Name = "みず",
                    SupperEffective = new[]{"ほのお","じめん","いわ"},
                    BadEffective = new []{ "みず", "くさ" , "ドラゴン"},
                },

                new TypeData()
                {
                    Name = "でんき",
                    SupperEffective = new[]{"みず","ひこう"},
                    BadEffective = new []{ "でんき", "くさ" , "ドラゴン"},
                    NoEffective = new []{"じめん"}
                },

                new TypeData()
                {
                    Name = "くさ",
                    SupperEffective = new[]{"みず","じめん","いわ"},
                    BadEffective = new []{ "ほのお", "くさ" , "どく","ひこう","むし","ドラゴン","はがね"},
                },

                new TypeData()
                {
                    Name = "こおり",
                    SupperEffective = new[]{"くさ","じめん","ひこう","ドラゴン"},
                    BadEffective = new []{ "ほのお", "みず" , "こおり","はがね"},
                },

                new TypeData()
                {
                    Name = "かくとう",
                    SupperEffective = new[]{"ノーマル","こおり","いわ","あく","はがね"},
                    BadEffective = new []{ "どく", "ひこう" , "エスパー","むし","フェアリー"},
                    NoEffective = new []{"ゴースト"},
                },

                new TypeData()
                {
                    Name = "どく",
                    SupperEffective = new[]{"くさ","フェアリー"},
                    BadEffective = new []{ "どく", "じめん" , "いわ","ゴースト"},
                    NoEffective = new []{"はがね"},
                },

                new TypeData()
                {
                    Name = "じめん",
                    SupperEffective = new[]{ "ほのお","でんき", "どく", "いわ","はがね"},
                    BadEffective = new []{ "くさ", "むし"},
                    NoEffective = new []{"ひこう"},
                },

                new TypeData()
                {
                    Name = "ひこう",
                    SupperEffective = new[]{ "くさ","かくとう", "むし"},
                    BadEffective = new []{ "でんき", "いわ", "はがね"},
                },

                new TypeData()
                {
                    Name = "エスパー",
                    SupperEffective = new[]{ "かくとう","どく"},
                    BadEffective = new []{ "エスパー", "はがね"},
                    NoEffective = new []{"あく"},
                },

                new TypeData()
                {
                    Name = "むし",
                    SupperEffective = new[]{ "くさ","エスパー","あく"},
                    BadEffective = new []{ "ほのお", "かくとう","どく","ひこう","ゴースト","はがね","フェアリー"},
                },

                new TypeData()
                {
                    Name = "いわ",
                    SupperEffective = new[]{ "ほのお","ひこう","こおり","むし"},
                    BadEffective = new []{ "かくとう", "じめん","はがね"},
                },

                new TypeData()
                {
                    Name = "ゴースト",
                    SupperEffective = new[]{ "エスパー","ゴースト"},
                    BadEffective = new []{ "あく"},
                    NoEffective = new []{"ノーマル"},
                },

                new TypeData()
                {
                    Name = "ドラゴン",
                    SupperEffective = new[]{ "ドラゴン"},
                    BadEffective = new []{ "はがね"},
                    NoEffective = new []{"フェアリー"},
                },

                new TypeData()
                {
                    Name = "あく",
                    SupperEffective = new[]{ "エスパー","ゴースト"},
                    BadEffective = new []{ "かくとう","あく","フェアリー"},
                    NoEffective = new []{"ノーマル"},
                },

                new TypeData()
                {
                    Name = "はがね",
                    SupperEffective = new[]{ "こおり","いわ","フェアリー"},
                    BadEffective = new []{ "ほのお","みず","でんき","はがね"},
                },

                new TypeData()
                {
                    Name = "フェアリー",
                    SupperEffective = new[]{ "かくとう","ドラゴン","あく"},
                    BadEffective = new []{ "ほのお","どく","はがね"},
                },
            };
            data.SerializeToFile(FilePath.TypeDataPath);
        }

        /// <summary>
        /// jsonから特性リストを取得する
        /// </summary>
        public static AbilityData[] LoadAbilities()
        {
            return JsonExtensions.DeserializeFromFile<AbilityData[]>(FilePath.AbilitiesDataPath)
                ?? Array.Empty<AbilityData>();
        }

        /// <summary>
        /// jsonからTypeリストを取得する
        /// </summary>
        public static TypeData[] LoadTypes()
        {
            if(File.Exists(FilePath.TypeDataPath) is false)
                SaveTypeToJson();
            return JsonExtensions.DeserializeFromFile<TypeData[]>(FilePath.TypeDataPath);
        }

        /// <summary>
        /// jsonから性格リストを取得する
        /// </summary>
        public static PersonalityData[] LoadPersonarities()
        {
            if (File.Exists(FilePath.PersonalityDataPath) is false)
                SavePersonalityToJson();
            return JsonExtensions.DeserializeFromFile<PersonalityData[]>(FilePath.PersonalityDataPath);
        }

        /// <summary>
        /// jsonからポケモンリストを取得する
        /// </summary>
        public static PokemonData[] LoadPokemons()
        {
            return JsonExtensions.DeserializeFromFile<PokemonData[]>(FilePath.PokemonDataPath)
                ?? Array.Empty<PokemonData>();
        }

        /// <summary>
        /// jsonからポケモンリストを取得する
        /// </summary>
        public static PokemonData[] LoadForms()
        {
            return JsonExtensions.DeserializeFromFile<PokemonData[]>(FilePath.PokemonFormDataPath)
                ?? Array.Empty<PokemonData>();
        }

        /// <summary>
        /// jsonからわざリストを取得する
        /// </summary>
        public static MoveData[] LoadMoves()
        {
            return JsonExtensions.DeserializeFromFile<MoveData[]>(FilePath.MovesDataPath) 
                ?? Array.Empty<MoveData>();
        }
    }
}
