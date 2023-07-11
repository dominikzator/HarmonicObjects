using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
