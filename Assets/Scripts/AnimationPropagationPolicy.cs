using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class AnimationPropagationPolicy
{
    [Inject] private GridHolder gridHolder;

    public GridHolder GridHolder => gridHolder;
    
    public abstract IEnumerable<AnimationComponent<T>> GetNext<T>(AnimationComponent<T> animationComponent) where T : AnimationPropagationPolicy, new();

    public abstract void Reset();
}
