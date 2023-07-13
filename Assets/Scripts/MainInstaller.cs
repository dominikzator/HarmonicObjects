using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private GlobalReferencesHolder globalReferencesHolder;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GridHolder>().AsSingle();
        Container.BindInterfacesAndSelfTo<AllNeighboursPolicy>().AsSingle();
        Container.BindInterfacesAndSelfTo<LeftPolicy>().AsSingle();
        Container.BindInterfacesAndSelfTo<RightPolicy>().AsSingle();
        Container.BindInterfacesAndSelfTo<UpPolicy>().AsSingle();
        Container.BindInterfacesAndSelfTo<DownPolicy>().AsSingle();
        Container.BindInterfacesAndSelfTo<RandomPolicy>().AsSingle();
        Container.BindInterfacesAndSelfTo<OneByOnePolicy>().AsSingle();
        Container.Bind<GlobalReferencesHolder>().FromInstance(globalReferencesHolder).AsSingle().NonLazy();
    }
}
