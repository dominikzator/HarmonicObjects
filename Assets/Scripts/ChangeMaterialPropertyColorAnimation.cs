using System;
using UnityEngine;

public class ChangeMaterialPropertyColorAnimation : AnimationComponent<AllNeighboursPolicy>
{
    [SerializeField] private string propertyName;
    [SerializeField] private Color colorToChange;

    public override void Animate()
    {
        if (Triggered)
        {
            return;
        }
        base.Animate();
        Renderer.material.SetColor(propertyName, colorToChange);
    }
    private void Start()
    {
        
    }
}
