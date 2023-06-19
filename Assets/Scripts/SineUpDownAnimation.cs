using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SineUpDownAnimation : AnimationComponent<RandomPolicy>
{
    [SerializeField] private float amplitude;

    private Vector3 startingPos;
    
    private bool triggered = false;
    private float index;
    
    private void Awake()
    {
        base.Awake();
        startingPos = gameObject.transform.position;
    }
    private void Update()
    {
        if (triggered)
        {
            index += Time.deltaTime;
            float y = amplitude * Mathf.Sin (AnimSpeed*index);
            Rigidbody.transform.position =
                new Vector3(Rigidbody.transform.position.x, y, Rigidbody.transform.position.z);
        }
    }
    
    public override IEnumerator Animate()
    {
        triggered = true;
        return base.Animate();
    }
}
