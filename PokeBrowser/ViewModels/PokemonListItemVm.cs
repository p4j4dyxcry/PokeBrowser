using PokeBrowser.Data;
using PokeBrowser.Models;

namespace PokeBrowser.ViewModels
{
    public class PokemonListItemVm : PokemonVm
    {
        private bool _isVisible = true;
        public bool IsVisible 
        {
            get => _isVisible;
            set => RaisePropertyChangedIfSet(ref _isVisible, value);
        }

        private int _id ;
        public int ID
        {
            get => _id;
            set => RaisePropertyChangedIfSet(ref _id, value);
        }

        public int Fastest { get; }
        public int SemiFastest { get; }
        public int FastestScarf => Fastest * 3 / 2;
        public int DefaultSpeed { get; }
        public int Latest { get; }

        public PokemonListItemVm(PokemonData pokemonData) : base(pokemonData)
        {
            ID = pokemonData.Id;
            Fastest = StatusCalculator.CalcParameter(pokemonData.Speed, 31, 252, 1.1, 50);
            DefaultSpeed = StatusCalculator.CalcParameter(pokemonData.Speed, 31, 0, 1.0, 50);
            SemiFastest = StatusCalculator.CalcParameter(pokemonData.Speed, 31, 252, 1.0, 50);
            Latest = StatusCalculator.CalcParameter(pokemonData.Speed,  0,   0, 0.9, 50);
        }
    }
}
