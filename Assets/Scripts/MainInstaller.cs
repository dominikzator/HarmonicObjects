using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private ObjectAnimator harmonicObjectAnimator;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PositionCalculator>().AsSingle().NonLazy();
    }
}
