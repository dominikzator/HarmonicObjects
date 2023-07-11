using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Inject] private readonly DiContainer mainContainer;
    [Inject] private GlobalReferencesHolder globalReferencesHolder;

    protected DiContainer MainContainer => mainContainer;

    public float AnimSpeed => animSpeed;
    public Rigidbody Rigidbody => rigidbody;
    public Renderer Renderer => renderer;

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

    protected void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        policy = mainContainer.Instantiate<T>();
    }

    public virtual void Animate()
    {
        if (Triggered)
        {
            return;
        }
        Triggered = true;
    }

    protected IEnumerator StartAnimateAsync(AnimationPropagationPolicy policy)
    {
        Debug.Log("StartAnimateAsync 1");
        Animate();
        foreach (var next in policy.GetNext(this))
        {
            Debug.Log("foreach 1");
            yield return new WaitForSeconds(globalReferencesHolder.ElementsDelay);
            
            foreach (var nextSingleElem in next.Where(p => !p.GetComponent<AnimationComponent<T>>().Triggered))
            {
                Debug.Log("foreach 2");
                var animComp = nextSingleElem.GetComponent<AnimationComponent<T>>();
                animComp.Animate();
                //foreach (var animComp in animComps)
                //{
                //    //Debug.Log("foreach 3");
                //    animComp.Animate();
                //}
            }
        }

        yield return null;
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
