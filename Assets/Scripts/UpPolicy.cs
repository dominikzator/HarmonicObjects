using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class UpPolicy : AnimationPropagationPolicy
{
    public override IEnumerable<GridElement> GetNext<T>(AnimationComponent<T> animationComponent)
    {
        GridElement gridElement = animationComponent.GetComponent<GridElement>();
        
        for (int i = gridElement.ColumnIndex + 1; i < GridHolder.ColumnCount; i++)
        {
            GameObject nextElementObj = GridHolder.Grid[gridElement.RowIndex, i].gameObject;
            yield return nextElementObj.GetComponent<GridElement>();
        }
    }
}
