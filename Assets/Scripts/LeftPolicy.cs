using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class LeftPolicy : AnimationPropagationPolicy
{
    public override bool GetPredicateMethod(GridElement elem)
    {
        return elem.RowIndex <= InitialElement.GetComponent<GridElement>().RowIndex && elem.ColumnIndex == InitialElement.GetComponent<GridElement>().ColumnIndex;
    }

    public override IEnumerable<GridElement> GetNext<T>(AnimationComponent<T> animationComponent)
    {
        Debug.Log("GetNext");
        GridElement gridElement = animationComponent.GetComponent<GridElement>();

        if (!Initialized)
        {
            Initialized = true;
            InitialElement = gridElement;
        }

        if (gridElement.RowIndex - 1 >= 0)
        {
            GameObject nextElementObj = GridHolder.Grid[gridElement.RowIndex - 1, gridElement.ColumnIndex].gameObject;

            if (!nextElementObj.GetComponent<AnimationComponent<T>>().AnimFlag && GetPredicateMethod(nextElementObj.GetComponent<GridElement>()))
            {
                yield return nextElementObj.GetComponent<GridElement>();
            }
        }
    }
}
