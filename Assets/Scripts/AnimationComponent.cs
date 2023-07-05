using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public abstract class AnimationComponent<T1, T2, T3, T4> : AnimationComponent<T1, T2, T3>
    where T1 : AnimationPropagationPolicy, new()
    where T2 : AnimationPropagationPolicy, new()
    where T3 : AnimationPropagationPolicy, new()
    where T4 : AnimationPropagationPolicy, new()
{
    private T4 fourthPolicy;

    public T4 FourthPolicy => fourthPolicy;
    
    public override List<AnimationPropagationPolicy> Policies => new() { Policy, SecondPolicy, ThirdPolicy, fourthPolicy };

    protected void Awake()
    {
        base.Awake();
        fourthPolicy = MainContainer.Instantiate<T4>();
    }
}

public abstract class AnimationComponent<T1, T2, T3> : AnimationComponent<T1, T2>
    where T1 : AnimationPropagationPolicy, new()
    where T2 : AnimationPropagationPolicy, new()
    where T3 : AnimationPropagationPolicy, new()
{
    private T3 thirdPolicy;

    public T3 ThirdPolicy => thirdPolicy;
    
    public override List<AnimationPropagationPolicy> Policies => new() { Policy, SecondPolicy, thirdPolicy };

    protected void Awake()
    {
        base.Awake();
        thirdPolicy = MainContainer.Instantiate<T3>();
    }
}

public abstract class AnimationComponent<T1, T2> : AnimationComponent<T1>
    where T1 : AnimationPropagationPolicy, new()
    where T2 : AnimationPropagationPolicy, new()
{
    private T2 secondPolicy;

    public T2 SecondPolicy => secondPolicy;

    public override List<AnimationPropagationPolicy> Policies => new() { Policy, secondPolicy };

    protected void Awake()
    {
        base.Awake();
        secondPolicy = MainContainer.Instantiate<T2>();
    }
}

public abstract class AnimationComponent<T> : ClickableComponent, IAnimated
where T : AnimationPropagationPolicy, new()
{
    [SerializeField] private float animSpeed;

    private T policy;
    
    private Rigidbody rigidbody;
    private Renderer renderer;
    private bool triggered = false;
    private bool animFlag = false;

    [Inject] private readonly DiContainer mainContainer;
    [Inject] private GlobalReferencesHolder globalReferencesHolder;

    protected DiContainer MainContainer => mainContainer;

    public float AnimSpeed => animSpeed;
    public Rigidbody Rigidbody => rigidbody;

    public T Policy => policy;
    
    public virtual List<AnimationPropagationPolicy> Policies => new(){policy};

    public bool Triggered
    {
        get
        {
            return triggered;
        }
        set
        {
            triggered = value;
        }
    }
    public bool AnimFlag
    {
        get
        {
            return animFlag;
        }
        set
        {
            animFlag = value;
        }
    }
    
    protected void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        policy = mainContainer.Instantiate<T>();
    }

    public virtual void Animate()
    {
        renderer.material.color = new Color(0f, 1f, 0f, 1f);
        AnimFlag = true;
    }

    protected IEnumerator StartAnimateAsync(AnimationPropagationPolicy policy)
    {
        Debug.Log("StartAnimateAsync 1");
        Animate();
        foreach (var next in policy.GetNext(this).Select(p => p.GetComponent<AnimationComponent<T>>()))
        {
            yield return new WaitForSeconds(globalReferencesHolder.ElementsDelay);
            next.Animate();
        }
        Debug.Log("StartAnimateAsync 2");
    }

    public override void OnClick()
    {
        base.OnClick();
        foreach (var policy in Policies)
        {
            StartCoroutine(StartAnimateAsync(policy));
        }
    }
}
