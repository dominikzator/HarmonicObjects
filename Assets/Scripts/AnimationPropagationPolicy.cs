using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class AnimationPropagationPolicy : IInitializable
{
    [Inject] private GridHolder gridHolder;
    public GridHolder GridHolder => gridHolder;

    public abstract IEnumerable<IEnumerable<GridElement>> GetNext(AnimationComponent animationComponent);
    public abstract void Initialize();
}
