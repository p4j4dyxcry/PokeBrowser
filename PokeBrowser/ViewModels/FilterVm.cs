using PokeBrowser.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeBrowser.ViewModels
{
    public class FilterVm : Livet.ViewModel
    {
        public string Label { get; }

        private bool _isEnabled;
        public bool IsEnabled
        { 
            get => _isEnabled;
            set => RaisePropertyChangedIfSet(ref _isEnabled , value); 
        }
        private readonly Func<PokemonData, bool> _filter;
        public FilterVm(string label , Func<PokemonData,bool> filter)
        {
            Label = label;
            _filter = filter;
        }

        public bool Filter(PokemonData pokemonData)
        {
            if (_isEnabled is false) return true;
            return _filter(pokemonData);
        }
        public override string ToString()
        {
            return Label;
        }
    }
}
