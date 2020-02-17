using PokeBrowser.Data;
using PokeBrowser.Models;
using Reactive.Bindings;

namespace PokeBrowser.ViewModels
{
    public class PokemonDetailVm : PokemonVm
    {
        /// <summary>
        /// 性格
        /// </summary>
        public IReactiveProperty<string> Personality { get; }

        /// <summary>
        /// 努力値
        /// </summary>
        public ParameterVm Ev { get; }

        /// <summary>
        /// 個体値
        /// </summary>
        public ParameterVm Iv { get; }

        public PokemonDetailVm(PokemonData pokemonData) : base(pokemonData)
        {
            Ev = new ParameterVm(new ParameterData<int>());
            Iv = new ParameterVm(new ParameterData<int>(31,31,31,31,31,31));
        }
    }
}