using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class RandomPolicy : AnimationPropagationPolicy
{
    private static GameObject[] objectsToAnimate;
    public override IEnumerable<GridElement> GetNext<T>(AnimationComponent<T> animationComponent)
    {
        IEnumerable<AnimationComponent<T>> objects = (Initialized && objectsToAnimate != null) ? objectsToAnimate.Select(p => p.GetComponent<AnimationComponent<T>>()) : GridHolder.GetGridList().Select(p => p.GetComponent<AnimationComponent<T>>());
        
        List<AnimationComponent<T>> objectsList = objects.ToList();
        objectsList.Remove(animationComponent);
        
        objectsToAnimate = objectsList.Select(p => p.gameObject).ToArray();
        
        if (!Initialized)
        {
            Initialized = true;
        }
        
        if (objectsToAnimate.Length == 0)
        {
            yield break;
        }
        
        Random r = new Random();
        int randInd = r.Next(0, objectsList.Count);

        yield return objectsList[randInd].GetComponent<GridElement>();
    }
}
