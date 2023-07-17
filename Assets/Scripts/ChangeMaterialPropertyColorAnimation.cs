using UnityEngine;

public class ChangeMaterialPropertyColorAnimation : AnimationComponent<AllNeighboursPolicy>
{
    [SerializeField] private string propertyName;
    [SerializeField] private bool randomColor;
    [SerializeField] private Color colorToChange;

    public override void Animate()
    {
        if (Triggered)
        {
            return;
        }
        base.Animate();
        if (randomColor)
        {
            colorToChange = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        }
        Renderer.material.SetColor(propertyName, colorToChange);
    }
}
