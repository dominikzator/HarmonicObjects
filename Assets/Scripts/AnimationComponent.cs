using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
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

    protected void Awake()
    {
        base.Awake();
        fourthPolicy = MainContainer.Instantiate<T4>();
    }
    
    public override IEnumerable<GridElement> GetNextElements()
    {
        return Policy.GetNext(this).Concat(SecondPolicy.GetNext(this)).Concat(ThirdPolicy.GetNext(this)).Concat(FourthPolicy.GetNext(this)).Distinct();
    }
}

public abstract class AnimationComponent<T1, T2, T3> : AnimationComponent<T1, T2>
    where T1 : AnimationPropagationPolicy, new()
    where T2 : AnimationPropagationPolicy, new()
    where T3 : AnimationPropagationPolicy, new()
{
    private T3 thirdPolicy;

    public T3 ThirdPolicy => thirdPolicy;

    protected void Awake()
    {
        base.Awake();
        thirdPolicy = MainContainer.Instantiate<T3>();
    }
    
    public override IEnumerable<GridElement> GetNextElements()
    {
        return Policy.GetNext(this).Concat(SecondPolicy.GetNext(this)).Concat(ThirdPolicy.GetNext(this)).Distinct();
    }
}

public abstract class AnimationComponent<T1, T2> : AnimationComponent<T1>
    where T1 : AnimationPropagationPolicy, new()
    where T2 : AnimationPropagationPolicy, new()
{
    private T2 secondPolicy;

    public T2 SecondPolicy => secondPolicy;

    protected void Awake()
    {
        base.Awake();
        secondPolicy = MainContainer.Instantiate<T2>();
    }
    
    public override IEnumerable<GridElement> GetNextElements()
    {
        return Policy.GetNext(this).Concat(secondPolicy.GetNext(this)).Distinct();
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

    public virtual IEnumerable<GridElement> GetNextElements()
    {
        return Policy.GetNext(this);
    }

    public virtual IEnumerator Animate()
    {
        if (AnimationPropagationPolicy.InitialElement == null)
        {
            Debug.Log($"Set InitialElement x: {gameObject.GetComponent<GridElement>().RowIndex}, y: {gameObject.GetComponent<GridElement>().ColumnIndex}");
            AnimationPropagationPolicy.InitialElement = gameObject.GetComponent<GridElement>();
        }
        renderer.material.color = new Color(0f, 1f, 0f, 1f);
        

        foreach (var animComponent in GetNextElements().Select(p => p.GetComponent<AnimationComponent<T>>()).Distinct())
        {
            animComponent.AnimFlag = true;
            
            yield return new WaitForSeconds(globalReferencesHolder.ElementsDelay);
            //yield return animComponent.Animate();
            var cor = StartCoroutine(animComponent.Animate());
        }

        yield return null;
    }

    private IEnumerator StartAnimate()
    {
        yield return Animate();
        
        Debug.Log("StartAnimate OnClick End");
    }

    public override void OnClick()
    {
        base.OnClick();
        StartCoroutine(StartAnimate());
    }
}
