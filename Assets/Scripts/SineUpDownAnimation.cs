using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

public class SineUpDownAnimation : AnimationComponent<LeftPolicy, RightPolicy, UpPolicy, DownPolicy>
{
    [SerializeField] private float amplitude;

    private Vector3 startingPos;
    
    private float index;
    
    private void Awake()
    {
        base.Awake();
        startingPos = gameObject.transform.position;
    }
    private void LateUpdate()
    {
        if (Triggered)
        {
            index += Time.deltaTime;
            float y = amplitude * Mathf.Sin (AnimSpeed*index);
            Rigidbody.transform.position =
                new Vector3(Rigidbody.transform.position.x, y, Rigidbody.transform.position.z);
        }
    }
    
    public override IEnumerator Animate()
    {
        Triggered = true;
        return base.Animate();
    }
}
