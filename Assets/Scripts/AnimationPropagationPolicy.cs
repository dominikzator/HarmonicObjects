using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;
using Zenject;

public abstract class AnimationPropagationPolicy
{
    private static bool initialized = false;
    private static List<GameObject> objectsAnimated = new List<GameObject>();
    
    public static GridElement InitialElement { get; set;}

    protected static bool Initialized
    {
        get => initialized;
        set => initialized = value;
    }
    public static List<GameObject> ObjectsAnimated => objectsAnimated;
    
    [Inject] private GridHolder gridHolder;

    public GridHolder GridHolder => gridHolder;

    //public Func<GridElement, bool> Condition;

    public abstract IEnumerable<GridElement> GetNext<T>(AnimationComponent<T> animationComponent) where T : AnimationPropagationPolicy, new();

    protected virtual void Initialize() { }

    public virtual void Reset()
    {
        Initialized = false;
        InitialElement = null;
        objectsAnimated.Clear();
    }
}
