using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotateAnimation : AnimationComponent<AllNeighboursPolicy>
{
    [SerializeField] private bool randomAngles;
    [SerializeField] private float xAngle;
    [SerializeField] private float yAngle;
    [SerializeField] private float zAngle;
    [SerializeField] private bool isInfinite;
    public override void Animate()
    {
        if (Triggered)
        {
            return;
        }
        base.Animate();
    }

    private void Update()
    {
        if (!Triggered)
        {
            return;
        }

        if (randomAngles)
        {
            xAngle = Random.Range(0f, 1f);
            yAngle = Random.Range(0f, 1f);
            zAngle = Random.Range(0f, 1f);
        }
        gameObject.transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
    }
}
