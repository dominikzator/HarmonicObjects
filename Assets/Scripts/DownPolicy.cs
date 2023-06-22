using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class DownPolicy : AnimationPropagationPolicy
{
    private static GridElement initialElement;
    private static bool initialized = false;
    
    public override IEnumerable<GridElement> GetNext<T>(AnimationComponent<T> animationComponent)
    {
        GridElement gridElement = animationComponent.GetComponent<GridElement>();

        if (gridElement.ColumnIndex - 1 >= 0)
        {
            GameObject nextElementObj = GridHolder.Grid[gridElement.RowIndex, gridElement.ColumnIndex - 1].gameObject;

            if (!nextElementObj.GetComponent<AnimationComponent<T>>().AnimFlag)
            {
                yield return nextElementObj.GetComponent<GridElement>();
            }
        }
    }
    public override void Reset()
    {
        initialized = false;
    }
}
