using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class RandomPolicy : AnimationPropagationPolicy
{
    public override IEnumerable<GridElement> GetNext<T>(AnimationComponent<T> animationComponent)
    {
        List<AnimationComponent<T>> objects = GridHolder.GetGridList().Select(p => p.GetComponent<AnimationComponent<T>>()).ToList();
        
        objects.Remove(animationComponent);
        
        while (objects.Count > 0)
        {
            Random r = new Random();
            int randInd = r.Next(0, objects.Count);
            AnimationComponent<T> next = objects[randInd];

            objects.Remove(next);
            yield return next.GetComponent<GridElement>();
        }
    }
}
