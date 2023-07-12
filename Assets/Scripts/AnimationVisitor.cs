using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AnimationVisitor : IInitializable, IDisposable, ITickable
{
    private AnimationPropagationPolicy animationPropagationPolicy;
    private List<AnimationComponent> animationComponents = new List<AnimationComponent>();

    [Inject] private GlobalReferencesHolder globalReferencesHolder;

    public GlobalReferencesHolder GlobalReferencesHolder => globalReferencesHolder;

    //public AnimationVisitor()
    //{
    //    Debug.Log("AnimationVisitor Default Constructor");
    //}

    public AnimationVisitor(AnimationPropagationPolicy policy, List<AnimationComponent> animationComponents)
    {
        Debug.Log($"AnimationVisitor Constructor policy: {policy} animationComponents count: {animationComponents.Count}");
        this.animationPropagationPolicy = policy;
        this.animationComponents = animationComponents;

        //Debug.Log("globalReferencesHolder.gameObject.name: " + globalReferencesHolder.gameObject.name);
    }

    public void Initialize()
    {
        Debug.Log("Initialize AnimationVisitor with Policy: " + animationPropagationPolicy.GetType());
    }

    public void Dispose()
    {
        Debug.Log("Dispose AnimationVisitor with Policy: " + animationPropagationPolicy.GetType());
    }

    public void Tick()
    {
        Debug.Log("Tick!");
    }
}
