using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class VisitorManager : MonoBehaviour, IPointerClickHandler, IClickable
{
    private Dictionary<AnimationPropagationPolicy, List<AnimationComponent>> animationsByPolicyDict = new Dictionary<AnimationPropagationPolicy, List<AnimationComponent>>();   //ID AnimationPropagationPolicy name

    [Inject] private DiContainer globalContainer;
    
    void Start()
    {
        InitializeAnimationsDict();
    }

    private void InitializeAnimationsDict()
    {
        var animComps = GetComponents<AnimationComponent>();

        foreach (var animComp in animComps)
        {
            foreach (var policy in animComp.Policies)
            {
                if (animationsByPolicyDict.Keys.Select(p => p.ToString()).Contains(policy.ToString()))
                {
                    try
                    {
                        if (!animationsByPolicyDict[policy].Contains(animComp))
                        {
                            animationsByPolicyDict[policy].Add(animComp);
                        }
                    }
                    catch (Exception e)
                    {
                        var key = animationsByPolicyDict.Keys.First(p => p.ToString() == policy.ToString());
                        animationsByPolicyDict[key].Add(animComp);
                    }
                }
                else
                {
                    animationsByPolicyDict.Add(policy, new List<AnimationComponent>{animComp});
                }
            }
        }
    }

    private IEnumerator StartAnimationVisitors()
    {
        foreach (var list in animationsByPolicyDict)
        {
            IEnumerable<object> args = new[] { list.Key, (object)animationsByPolicyDict[list.Key]};
            AnimationVisitor animationVisitor = globalContainer.Instantiate<AnimationVisitor>(args);
            
            foreach (var animationComponent in animationVisitor.AnimationComponents)
            {
                StartCoroutine(animationVisitor.StartAnimateAsync(animationComponent));
            }
        }

        yield return null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }

    public void OnClick()
    {
        StartCoroutine(StartAnimationVisitors());
    }
}
