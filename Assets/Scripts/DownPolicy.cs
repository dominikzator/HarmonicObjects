using System.Collections.Generic;
using UnityEngine;

public class DownPolicy : AnimationPropagationPolicy
{
    public override IEnumerable<IEnumerable<GridElement>> GetNext(AnimationComponent animationComponent)
    {
        GridElement gridElement = animationComponent.GetComponent<GridElement>();
        
        for (int i = gridElement.ColumnIndex - 1; i >= 0; i--)
        {
            GameObject nextElementObj = GridHolder.Grid[gridElement.RowIndex, i].gameObject;
            IEnumerable<GridElement> output = new List<GridElement> { nextElementObj.GetComponent<GridElement>() };
            yield return output;
        }
    }
}
