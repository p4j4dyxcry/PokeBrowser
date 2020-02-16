using PokeBrowser.Data;
using PokeBrowser.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

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
        public int Lastest { get; }

        public PokemonListItemVm(PokemonData pokemonData) : base(pokemonData)
        {
            ID = pokemonData.Id;
            Fastest = Calcrator.CalcParametor(pokemonData.Speed, 31, 252, 1.1, 50);
            DefaultSpeed = Calcrator.CalcParametor(pokemonData.Speed, 31, 0, 1.0, 50);
            SemiFastest = Calcrator.CalcParametor(pokemonData.Speed, 31, 252, 1.0, 50);
            Lastest = Calcrator.CalcParametor(pokemonData.Speed,  0,   0, 0.9, 50);
        }
    }
}
