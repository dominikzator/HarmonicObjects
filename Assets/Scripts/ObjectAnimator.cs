using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectAnimator : MonoBehaviour
{
    private Rigidbody rigidbody;

    [Inject] private PositionCalculator positionCalculator;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
    }

    public void LateUpdate(){

        rigidbody.transform.position = new Vector3(rigidbody.transform.position.x,positionCalculator.CurrentY,rigidbody.transform.position.z);
    }
}
