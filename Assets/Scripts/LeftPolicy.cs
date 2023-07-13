using System.Collections.Generic;
using UnityEngine;

public class LeftPolicy : AnimationPropagationPolicy
{
    public override IEnumerable<IEnumerable<GridElement>> GetNext(AnimationComponent animationComponent)
    {
        GridElement gridElement = animationComponent.GetComponent<GridElement>();

        for (int i = gridElement.RowIndex - 1; i >= 0; i--)
        {
            GameObject nextElementObj = GridHolder.Grid[i, gridElement.ColumnIndex].gameObject;
            IEnumerable<GridElement> output = new List<GridElement> { nextElementObj.GetComponent<GridElement>() };
            yield return output;
        }
    }
}
