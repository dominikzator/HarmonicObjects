using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneByOnePolicy : AnimationPropagationPolicy
{
    private static GridElement startingGridElement = null;
    public override IEnumerable<GridElement> GetNext<T>(AnimationComponent<T> animationComponent)
    {
        GridElement gridElement = animationComponent.GetComponent<GridElement>();

        if (!startingGridElement)
        {
            startingGridElement = gridElement;
        }
        ObjectsAnimated.Add(animationComponent.gameObject);

        int rowInd = gridElement.RowIndex;
        int columnInd = gridElement.ColumnIndex;
        rowInd++;
        if (rowInd >= GridHolder.RowCount)
        {
            rowInd = 0;
            columnInd++;
            if (columnInd >= GridHolder.ColumnCount)
            {
                rowInd = 0;
                columnInd = 0;
            }
        }

        AnimationComponent<T> nextElement = GridHolder.Grid[rowInd, columnInd].GetComponent<AnimationComponent<T>>();

        if (ReferenceEquals(startingGridElement, nextElement.GetComponent<GridElement>()))
        {
            yield break;
        }
        
        yield return GridHolder.Grid[rowInd, columnInd].GetComponent<GridElement>();
    }
    public override void Reset()
    {
        startingGridElement = default;
    }
}
