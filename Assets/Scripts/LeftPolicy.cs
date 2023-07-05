using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class LeftPolicy : AnimationPropagationPolicy
{
    public override IEnumerable<GridElement> GetNext<T>(AnimationComponent<T> animationComponent)
    {
        GridElement gridElement = animationComponent.GetComponent<GridElement>();

        for (int i = gridElement.RowIndex - 1; i >= 0; i--)
        {
            GameObject nextElementObj = GridHolder.Grid[i, gridElement.ColumnIndex].gameObject;
            yield return nextElementObj.GetComponent<GridElement>();
        }
    }
}
