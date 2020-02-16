using PokeBrowser.Data;
using System.Linq;
using System.Collections.Generic;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Linq;
using System;
using PokeBrowser.Foundation;
using PokeBrowser.Models;

namespace PokeBrowser.ViewModels
{
    public class MainWindowVm : Livet.ViewModel
    {
        public List<PokemonListItemVm> ItemsSource { get; }

        public List<FilterVm> IdFIlter { get; }

        public IReactiveProperty<string> FilterText { get; }
        public IReactiveProperty<bool> ShowMegaShinka { get; }
        public IReactiveProperty<bool> ShowBanLegend { get; }

        public MainWindowVm()
        {
            FilterText = new ReactivePropertySlim<string>(string.Empty).AddTo(CompositeDisposable);
            ShowMegaShinka = new ReactivePropertySlim<bool>(false).AddTo(CompositeDisposable);
            ShowBanLegend = new ReactivePropertySlim<bool>(false).AddTo(CompositeDisposable);

            var abilities = DataLoader.LoadAbilities();
            var types = DataLoader.LoadTypes();
            var persons = DataLoader.LoadPersonarities();
            var pokemons = DataLoader.LoadPokemons();
            var moves = DataLoader.LoadMoves();
            var forms = DataLoader.LoadForms();

            var db = DataBase.Build(abilities, types, persons, pokemons, moves, forms);
            DataBaseService.Initialize(db);

            ItemsSource = db.Pokemons
                .Select(x => new PokemonListItemVm(x))
                .Concat(db.Forms.Select(x=>new PokemonListItemVm(x)))
                .OrderBy(x=>x.ID)
                .ToList();

            IdFIlter = new List<FilterVm>()
            {
                new FilterVm("全国",(x)=>true),
                new FilterVm("ガラル",(x)=>x.GalarID != null),
            };
                
            IdFIlter
                .Select(x => x.ObserveProperty(x => x.IsEnabled))
                .Merge()
                .StartWith()
                .Where(x=>IdFIlter.Any(x=>x.IsEnabled))
                .Select(_=>IdFIlter.FirstOrDefault(x => x.IsEnabled))
                .Throttle(TimeSpan.FromMilliseconds(50))
                .ObserveOnUIDispatcher()
                .Do(x =>
                {
                    switch (x.Label)
                    {
                        case "全国":
                            foreach (var item in ItemsSource)
                                item.ID = item.Model.Id;
                            break;
                        case "ガラル":
                            foreach (var item in ItemsSource)
                                item.ID = item.Model.GalarID ?? -1;
                            break;
                    }
                })
                .Do(x => ApplyFilters())
                .Subscribe()
                .AddTo(CompositeDisposable);

            FilterText
                .Throttle(TimeSpan.FromMilliseconds(50))
                .ObserveOnUIDispatcher()
                .Subscribe(_ => Messenger.Raise(new Livet.Messaging.InteractionMessage("RaiseFilter")))
                .AddTo(CompositeDisposable);

            ShowMegaShinka
                .Merge(ShowBanLegend)
                .Throttle(TimeSpan.FromMilliseconds(5))
                .ObserveOnDispatcher()
                .Do(_ => ApplyFilters())
                .Subscribe();
        }

        private void ApplyFilters()
        {
            var idFilter = IdFIlter?.FirstOrDefault(x => x.IsEnabled);

            foreach (var item in ItemsSource)
                item.IsVisible = true;

            if(idFilter != null)
                Filter(idFilter.Filter);

            if (ShowMegaShinka.Value is false)
            {
                Filter(x=>!Filters.IsMegashinka(x));
            }
            if (ShowBanLegend.Value is false)
            {
                Filter(x => !Filters.IsBanLegend(x));
            }
            Messenger.Raise(new Livet.Messaging.InteractionMessage("RaiseFilter"));
        }

        private void Filter(Func<PokemonData,bool>filter)
        {
            foreach (var item in ItemsSource.Where(x => x.IsVisible))
            {
                item.IsVisible = filter(item.Model);
            }
        }

        public bool Filter(object obj)
        {
            if(obj is PokemonListItemVm pokemonVm)
            {
                if (pokemonVm.IsVisible is false)
                    return false;

                var values = FilterText.Value.Trim().Split(' ', '\n', '　');

                if (values.Length is 0)
                    return true;

                var targets = new[]
                {
                    pokemonVm.Name,
                    pokemonVm.Type1,
                    pokemonVm.Type2,
                    pokemonVm.Ability1,
                    pokemonVm.Ability2,
                    pokemonVm.Ability3,
                };

                return StringAnalizer.ContainByJajp(targets, values);
            }

            return true;
        }

    }
}
