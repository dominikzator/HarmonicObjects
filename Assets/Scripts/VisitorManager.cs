using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("gameObject.name: " + gameObject.name);
        var animComps = GetComponents<AnimationComponent>();

        foreach (var animComp in animComps)
        {
            Debug.Log("animComp: " + animComp.GetType());
        }
    }

}
