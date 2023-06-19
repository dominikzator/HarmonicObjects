using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DG.Tweening;
using UnityEngine;
using Zenject;

public abstract class AnimationComponent<T> : ClickableComponent, IAnimated
where T : AnimationPropagationPolicy, new()
{
    [SerializeField] private float animSpeed;

    private T policy;
    
    private Rigidbody rigidbody;
    
    [Inject] private readonly DiContainer mainContainer;
    [Inject] private GlobalReferencesHolder globalReferencesHolder;

    public float AnimSpeed => animSpeed;
    public Rigidbody Rigidbody => rigidbody;
    public T Policy => policy;
    
    public void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody>();
        policy = mainContainer.Instantiate<T>();
    }

    public virtual IEnumerator Animate()
    {
        GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 1f);
        
        foreach (var animComponent in Policy.GetNext(this))
        {
            yield return new WaitForSeconds(globalReferencesHolder.ElementsDelay);
            StartCoroutine(animComponent.Animate());
        }

        yield return null;
    }

    public override void OnClick()
    {
        base.OnClick();
        StartCoroutine(Animate());
    }
}
