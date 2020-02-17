using Livet;
using PokeBrowser.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace PokeBrowser.ViewModels
{
    public class ParameterVm : ViewModel
    {
        public IReactiveProperty<int> Hp { get; }
        public IReactiveProperty<int> Attack { get; }
        public IReactiveProperty<int> Defense { get; }
        public IReactiveProperty<int> SpecialAttack { get; }
        public IReactiveProperty<int> SpecialDefense { get; }
        public IReactiveProperty<int> Speed { get; }

        public ParameterData<int> Model { get; }

        public ParameterVm(ParameterData<int> model)
        {
            Model = model;
            Hp = model.ToReactivePropertyAsSynchronized(x => x.Hp).AddTo(CompositeDisposable);
            Attack = model.ToReactivePropertyAsSynchronized(x => x.Attack).AddTo(CompositeDisposable);
            Defense = model.ToReactivePropertyAsSynchronized(x => x.Defense).AddTo(CompositeDisposable);
            SpecialAttack = model.ToReactivePropertyAsSynchronized(x => x.SpecialAttack).AddTo(CompositeDisposable);
            SpecialDefense = model.ToReactivePropertyAsSynchronized(x => x.SpecialDefense).AddTo(CompositeDisposable);
            Speed = model.ToReactivePropertyAsSynchronized(x => x.Speed).AddTo(CompositeDisposable);
        }
    }
}