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

public abstract class AnimationComponent<T> : AnimationComponent
where T : AnimationPropagationPolicy, new()
{
    private T policy;

    public T Policy => policy;
    
    public override List<AnimationPropagationPolicy> Policies => new(){policy};
    
    protected void Awake()
    {
        base.Awake();
        policy = MainContainer.Instantiate<T>();
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
public abstract class AnimationComponent : ClickableComponent, IAnimated
{
    [SerializeField] private float animSpeed;
    
    private Rigidbody rigidbody;
    private Renderer renderer;
    private bool triggered = false;
    
    [Inject] private readonly DiContainer mainContainer;
    [Inject] private GlobalReferencesHolder globalReferencesHolder;

    protected DiContainer MainContainer => mainContainer;
    
    protected GlobalReferencesHolder GlobalReferencesHolder => globalReferencesHolder;

    public float AnimSpeed => animSpeed;
    public Rigidbody Rigidbody => rigidbody;
    public Renderer Renderer => renderer;
    
    public virtual List<AnimationPropagationPolicy> Policies => new(){};

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
    }
    
    protected IEnumerator StartAnimateAsync(AnimationPropagationPolicy policy)
    {
        Debug.Log("StartAnimateAsync 1");
        Animate();
        foreach (var next in policy.GetNext(this))
        {
            Debug.Log("foreach 1");
            yield return new WaitForSeconds(GlobalReferencesHolder.ElementsDelay);
            
            foreach (var nextSingleElem in next.Where(p => !p.GetComponent<AnimationComponent>().Triggered))
            {
                Debug.Log("foreach 2");
                var animComp = nextSingleElem.GetComponent<AnimationComponent>();
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

    public virtual void Animate()
    {
        if (Triggered)
        {
            return;
        }
        Triggered = true;
    }
}
