using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class RightPolicy : AnimationPropagationPolicy
{
    public override bool GetPredicateMethod(GridElement elem)
    {
        return elem.RowIndex >= InitialElement.GetComponent<GridElement>().RowIndex && elem.ColumnIndex == InitialElement.GetComponent<GridElement>().ColumnIndex;
    }
    public override IEnumerable<GridElement> GetNext<T>(AnimationComponent<T> animationComponent)
    {
        GridElement gridElement = animationComponent.GetComponent<GridElement>();

        if (gridElement.RowIndex + 1 < GridHolder.RowCount)
        {
            GameObject nextElementObj = GridHolder.Grid[gridElement.RowIndex + 1, gridElement.ColumnIndex].gameObject;

            if (!nextElementObj.GetComponent<AnimationComponent<T>>().AnimFlag && GetPredicateMethod(nextElementObj.GetComponent<GridElement>()))
            {
                yield return nextElementObj.GetComponent<GridElement>();
            }
        }
    }
}
