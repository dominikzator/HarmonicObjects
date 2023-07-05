using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class UpPolicy : AnimationPropagationPolicy
{
    public override bool GetPredicateMethod(GridElement elem)
    {
        return elem.RowIndex == InitialElement.GetComponent<GridElement>().RowIndex && elem.ColumnIndex >= InitialElement.GetComponent<GridElement>().ColumnIndex;
    }
    public override IEnumerable<GridElement> GetNext<T>(AnimationComponent<T> animationComponent)
    {
        GridElement gridElement = animationComponent.GetComponent<GridElement>();

        if (!Initialized)
        {
            Initialized = true;
            InitialElement = gridElement;
        }
        
        if (gridElement.ColumnIndex + 1 < GridHolder.ColumnCount)
        {
            GameObject nextElementObj = GridHolder.Grid[gridElement.RowIndex, gridElement.ColumnIndex + 1].gameObject;

            if (!nextElementObj.GetComponent<AnimationComponent<T>>().AnimFlag && GetPredicateMethod(nextElementObj.GetComponent<GridElement>()))
            {
                yield return nextElementObj.GetComponent<GridElement>();
            }
        }
    }
}
