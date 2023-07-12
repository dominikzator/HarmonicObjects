using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class VisitorManager : MonoBehaviour, IPointerClickHandler, IClickable
{
    private Dictionary<AnimationPropagationPolicy, List<AnimationComponent>> animationsByPolicyDict = new Dictionary<AnimationPropagationPolicy, List<AnimationComponent>>();   //ID AnimationPropagationPolicy name

    [Inject] private GlobalReferencesHolder globalReferencesHolder;
    [Inject] private DiContainer globalContainer;
    
    void Start()
    {
        InitializeAnimationsDict();
    }

    private void InitializeAnimationsDict()
    {
        Debug.Log("gameObject.name: " + gameObject.name);
        var animComps = GetComponents<AnimationComponent>();

        foreach (var animComp in animComps)
        {
            foreach (var policy in animComp.Policies)
            {
                //string policyString = policy.ToString();
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
        
        foreach (var dictElem in animationsByPolicyDict)
        {
            Debug.Log($"Key: {dictElem.Key}");
            foreach (var value in animationsByPolicyDict[dictElem.Key])
            {
                Debug.Log("value: " + value);
            }
        }
    }

    private IEnumerator StartAnimationVisitors()
    {
        Debug.Log("StartAnimationVisitors");
        foreach (var list in animationsByPolicyDict)
        {
            Debug.Log("list.Key: " + list.Key);
            IEnumerable<object> args = new[] { list.Key, (object)animationsByPolicyDict[list.Key]};
            AnimationVisitor animationVisitor = globalContainer.Instantiate<AnimationVisitor>(args);
            //globalContainer.Instantiate<AnimationVisitor>();

            //AnimationVisitor animationVisitor = new AnimationVisitor(list.Key, animationsByPolicyDict[list.Key]);
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
