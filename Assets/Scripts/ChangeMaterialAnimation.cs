using System;
using UnityEngine;

public class ChangeMaterialAnimation : AnimationComponent<AllNeighboursPolicy>
{
    [SerializeField] private Material materialToChange;

    public override void Animate()
    {
        if (Triggered)
        {
            return;
        }
        base.Animate();
        Renderer.material = materialToChange;
    }
    private void Start()
    {
        
    }
}
