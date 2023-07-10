using System.Collections.Generic;
using UnityEngine;

public class RightPolicy : AnimationPropagationPolicy
{
    public override IEnumerable<IEnumerable<GridElement>> GetNext<T>(AnimationComponent<T> animationComponent)
    {
        GridElement gridElement = animationComponent.GetComponent<GridElement>();
        
        for (int i = gridElement.RowIndex + 1; i < GridHolder.RowCount; i++)
        {
            GameObject nextElementObj = GridHolder.Grid[i, gridElement.ColumnIndex].gameObject;
            IEnumerable<GridElement> output = new List<GridElement> { nextElementObj.GetComponent<GridElement>() };
            yield return output;
        }
    }
}
