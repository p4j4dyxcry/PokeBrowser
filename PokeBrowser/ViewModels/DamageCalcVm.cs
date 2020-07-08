using System.Collections.Generic;
using Livet;
using PokeBrowser.Data;
using Reactive.Bindings;

namespace PokeBrowser.ViewModels
{
    public class DamageCalcVm : ViewModel
    {
        private PokemonData Model { get; }
        
        public IReactiveProperty<PokemonData> Target { get; }
        
        
        public IReactiveProperty<string> Name { get; }
        public IReactiveProperty<string> Personarity { get; }
        
        public IEnumerable<string> Items { get; }

        public IEnumerable<string> Mode { get; } = new[]
        {
            "防御側", "攻撃側",
        };
        
        public IReactiveProperty<int> IsDefence { get; }

        // 努力値
        public IReactiveProperty<int> EV { get; }
        
        public DamageCalcVm(PokemonData pokemonData)
        {
            Model = pokemonData;
            EV = new ReactivePropertySlim<int>(252);
            Name = new ReactivePropertySlim<string>("エースバーン");
            Personarity = new ReactivePropertySlim<string>("ようき");
            IsDefence = new ReactivePropertySlim<int>(0);
        }

        public void ChangeTarget(string name)
        {
            // todo
        }
        
    }
}