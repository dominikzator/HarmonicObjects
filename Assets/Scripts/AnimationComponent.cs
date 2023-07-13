using System.Collections.Generic;
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
}
public abstract class AnimationComponent : ClickableComponent, IAnimated
{
    [SerializeField] private float animDelay;
    [SerializeField] private float animSpeed;
    
    private Rigidbody rigidbody;
    private Renderer renderer;
    private bool triggered = false;
    
    [Inject] private readonly DiContainer mainContainer;

    protected DiContainer MainContainer => mainContainer;
    
    public float AnimDelay => animDelay;
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
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
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
