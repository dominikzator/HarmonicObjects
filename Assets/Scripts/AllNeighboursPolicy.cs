using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class AllNeighboursPolicy : AnimationPropagationPolicy
{
    public override IEnumerable<GridElement> GetNext<T>(AnimationComponent<T> animationComponent)
    {
        GridElement gridElement = animationComponent.GetComponent<GridElement>();

        if (!Initialized)
        {
            Initialized = true;
        }
        
        ObjectsAnimated.Add(animationComponent.gameObject);
        
        List<Vector2> neighbourCoordinates = new List<Vector2>();
        
        neighbourCoordinates.Add(new Vector2(gridElement.RowIndex - 1, gridElement.ColumnIndex));
        neighbourCoordinates.Add(new Vector2(gridElement.RowIndex, gridElement.ColumnIndex + 1));
        neighbourCoordinates.Add(new Vector2(gridElement.RowIndex, gridElement.ColumnIndex - 1));
        neighbourCoordinates.Add(new Vector2(gridElement.RowIndex + 1, gridElement.ColumnIndex));

        if (ObjectsAnimated.Count == GridHolder.RowCount * GridHolder.ColumnCount)
        {
            yield break;
        }

        List<AnimationComponent<T>> elements = GetValidElements(neighbourCoordinates).Select(p => p.GetComponent<AnimationComponent<T>>()).ToList();

        foreach (var elem in elements.Where(q => !q.AnimFlag))
        {
            yield return elem.GetComponent<GridElement>();
        }
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
