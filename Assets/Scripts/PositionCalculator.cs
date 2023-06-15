using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PositionCalculator : IInitializable, ITickable
{
    private float amplitudeY = 3f;
    private float omegaY = 3f;
    
    public float CurrentY;
    
    private float sinArgument;

    public void Initialize()
    {
        Debug.Log("PositionCalculator Initialize");
    }

    public void Tick()
    {
        sinArgument += Time.deltaTime;
        CurrentY= amplitudeY*Mathf.Sin (omegaY*sinArgument);
    }
}
