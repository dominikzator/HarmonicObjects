using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlobalReferencesHolder : MonoBehaviour
{
    [SerializeField] private float turnProbability;
    
    private List<Type> animationPropagationPolicyTypes = new List<Type>();

    public float TurnProbability => turnProbability;

    private void Start()
    {
        animationPropagationPolicyTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(p => p.IsSubclassOf(typeof(AnimationPropagationPolicy))).ToList();
    }

    public Type GetAnimationPropagationPolicy(string name)
    {
        return animationPropagationPolicyTypes.First(p => p.ToString() == name);
    }
}
