using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class LeftPolicy : AnimationPropagationPolicy
{
    private static List<GameObject> objectsAnimated = new List<GameObject>();
    private static GridElement initialElement;
    private static bool initialized = false;

    public override IEnumerable<GridElement> GetNext<T>(AnimationComponent<T> animationComponent)
    {
        //if (ObjectsAnimated.Contains(animationComponent.gameObject))
        //{
        //    yield break;
        //}
        //ObjectsAnimated.Add(animationComponent.gameObject);

        Debug.Log("Left GetNext");
        GridElement gridElement = animationComponent.GetComponent<GridElement>();

        if (gridElement.RowIndex - 1 >= 0)
        {
            yield return GridHolder.Grid[gridElement.RowIndex - 1, gridElement.ColumnIndex].GetComponent<GridElement>();
        }
    }
    public override void Reset()
    {
        initialized = false;
    }
    private List<GridElement> GetValidElements(List<Vector2> coordinates)
    {
        List<GridElement> gridElements = new List<GridElement>();

        foreach (var coord in coordinates)
        {
            if ((int)coord.x >= 0 && (int)coord.x < GridHolder.RowCount)
            {
                if ((int)coord.y >= 0 && (int)coord.y < GridHolder.ColumnCount)
                {
                    gridElements.Add(GridHolder.Grid[(int)coord.x, (int)coord.y].GetComponent<GridElement>());
                }
            }
        }
        return gridElements;
    }
}
