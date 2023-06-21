using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class AnimationPropagationPolicy
{
    private static bool initialized = false;
    private static List<GameObject> objectsAnimated = new List<GameObject>();

    protected static bool Initialized
    {
        get => initialized;
        set => initialized = value;
    }
    public static List<GameObject> ObjectsAnimated => objectsAnimated;
    
    [Inject] private GridHolder gridHolder;

    public GridHolder GridHolder => gridHolder;
    
    public abstract IEnumerable<GridElement> GetNext<T>(AnimationComponent<T> animationComponent) where T : AnimationPropagationPolicy, new();
    
    public abstract void Reset();
}
