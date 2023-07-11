using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllNeighboursPolicy : AnimationPropagationPolicy
{
    public override IEnumerable<IEnumerable<GridElement>> GetNext<T>(AnimationComponent<T> animationComponent)
    {
        Debug.Log("GetNext");
        GridElement gridElement = animationComponent.GetComponent<GridElement>();
        
        List<AnimationComponent<T>> objects = GridHolder.GetGridList().Select(p => p.GetComponent<AnimationComponent<T>>()).ToList();

        int iterations = Mathf.Max(GridHolder.RowCount, GridHolder.ColumnCount);
        int range = 1;

        for (int i = 0; i < iterations; i++)
        {
            range = i + 1;
            var neighbours = objects.Select(p => p.GetComponent<GridElement>()).Where(q =>
                (Mathf.Abs(q.RowIndex - gridElement.RowIndex) <= range && Mathf.Abs(q.ColumnIndex - gridElement.ColumnIndex) <= range)).ToList();

            foreach (var animComp in neighbours.Select(p => p.GetComponent<AnimationComponent<T>>()))
            {
                animComp.Triggered = true;
            }
            
            yield return neighbours.AsEnumerable();

            if (neighbours.Count == objects.Count)
            {
                yield break;
            }

            range++;
        }
    }
}
