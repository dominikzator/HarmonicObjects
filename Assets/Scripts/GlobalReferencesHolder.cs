using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GlobalReferencesHolder : MonoBehaviour
{
    [SerializeField] private float attractSpeed;
    [SerializeField] private float testingY;
    [SerializeField] private float elementsDelay;
    [SerializeField] private Material distortedHologramMaterial;

    public float AttractSpeed => attractSpeed;
    public float TestingY => testingY;
    public float ElementsDelay => elementsDelay;
    public Material DistortedHologramMaterial => distortedHologramMaterial;

    private List<Type> animationPropagationPolicyTypes = new List<Type>();

    private void Start()
    {
        animationPropagationPolicyTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(p => p.IsSubclassOf(typeof(AnimationPropagationPolicy))).ToList();
    }

    public Type GetAnimationPropagationPolicy(string name)
    {
        return animationPropagationPolicyTypes.First(p => p.ToString() == name);
    }
}
