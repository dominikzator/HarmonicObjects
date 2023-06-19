using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private GlobalReferencesHolder globalReferencesHolder;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GridHolder>().AsSingle().NonLazy();
        Container.Bind<GlobalReferencesHolder>().FromInstance(globalReferencesHolder).AsSingle().NonLazy();
    }
}
