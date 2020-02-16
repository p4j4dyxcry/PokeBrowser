using System.IO;

namespace PokeBrowser.Models
{
    static class FilePath
    {
        private static string _resources = null;

        private static string _findResourcePath()
        {
            var cd = Directory.GetCurrentDirectory();
            var resources = "Resources";

            while (Directory.Exists(Path.Combine(cd, resources)) is false)
            {
                cd = Directory.GetParent(cd).FullName;
            }
            return Path.Combine(cd, resources);
        }

        public static string Resources => _resources ??= _findResourcePath();

        public static string PokemonDataPath => Path.Combine(Resources, "pokemon_data.json");
        public static string PokemonFormDataPath => Path.Combine(Resources, "forms.json");

        public static string PokemonIconDirectoryPath => Path.Combine(Resources, "images");
        public static string AbilitiesDataPath => Path.Combine(Resources, "abilities.json");
        public static string MovesDataPath => Path.Combine(Resources, "moves.json");
        public static string PersonalityDataPath => Path.Combine(Resources, "personalities.json");
        public static string TypeDataPath => Path.Combine(Resources, "types.json");
    }
}
