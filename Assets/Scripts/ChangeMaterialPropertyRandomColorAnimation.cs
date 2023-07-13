using UnityEngine;

public class ChangeMaterialPropertyRandomColorAnimation : AnimationComponent<AllNeighboursPolicy>
{
    [SerializeField] private string propertyName;

    public override void Animate()
    {
        if (Triggered)
        {
            return;
        }
        base.Animate();

        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        Renderer.material.SetColor(propertyName, color);
    }
}
