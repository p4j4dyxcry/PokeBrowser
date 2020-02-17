using PokeBrowser.Data;
using Reactive.Bindings;

namespace PokeBrowser.ViewModels
{
    public class PokemonDetailVm : PokemonVm
    {
        /// <summary>
        /// 性格
        /// </summary>
        public IReactiveProperty<string> Personality { get; }

        public PokemonDetailVm(PokemonData pokemonData) : base(pokemonData)
        {
        }
    }
}