using System.Collections.Generic;
using UnityEngine;

public class RandomLeftPolicy : AnimationPropagationPolicy
{
    public enum MoveDirection
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    private MoveDirection currentDirection;
    private Dictionary<MoveDirection, List<Vector2>> transitions = new();
    
    public override IEnumerable<IEnumerable<GridElement>> GetNext(AnimationComponent animationComponent)
    {
        transitions.Clear();
        transitions.Add(MoveDirection.LEFT, new List<Vector2> { Vector2.up, Vector2.down});
        transitions.Add(MoveDirection.RIGHT, new List<Vector2> { Vector2.up, Vector2.down});
        transitions.Add(MoveDirection.UP, new List<Vector2> { Vector2.left});
        transitions.Add(MoveDirection.DOWN, new List<Vector2> { Vector2.left});
        
        GridElement gridElement = animationComponent.GetComponent<GridElement>();
        GridElement currentElement = gridElement;
        int iterations = 0;
        Vector2 dirVector = Vector2.zero;

        while (true)
        {
            if (iterations == 0)
            {
                dirVector = new Vector2(-1, 0);
            }
            else
            {
                float randomValue = Random.Range(0f, 1f);
                float turnProbability = (currentDirection == MoveDirection.LEFT) ? GlobalReferencesHolder.TurnProbability : 1f - GlobalReferencesHolder.TurnProbability;
                if (randomValue <= turnProbability)
                {
                    float randomDirValue = Random.Range(0f, 1f);
                    if (randomDirValue <= 0.5f)
                    {
                        dirVector = transitions[currentDirection][0];
                    }
                    else
                    {
                        if (transitions[currentDirection].Count > 1)
                        {
                            dirVector = transitions[currentDirection][1];
                        }
                    }
                }
            }
            GridElement nextElem = GetShiftedElement(currentElement, dirVector);
            if (nextElem == null)
            {
                yield break;
            }
            IEnumerable<GridElement> output = new List<GridElement> { nextElem.GetComponent<GridElement>() };
            yield return output;
            if (dirVector == Vector2.left)
            {
                currentDirection = MoveDirection.LEFT;
            }
            else if (dirVector == Vector2.right)
            {
                currentDirection = MoveDirection.RIGHT;
            }
            else if (dirVector == Vector2.down)
            {
                currentDirection = MoveDirection.DOWN;
            }
            else if (dirVector == Vector2.up)
            {
                currentDirection = MoveDirection.UP;
            }
            currentElement = nextElem;
            iterations++;
        }
    }

    private GridElement GetShiftedElement(GridElement elem, Vector2 shift)
    {
        int rowIndex = (int)(elem.RowIndex + shift.x);
        int columnIndex = (int)(elem.ColumnIndex + shift.y);

        if (rowIndex < 0 || columnIndex < 0 || rowIndex >= GridHolder.RowCount || columnIndex >= GridHolder.ColumnCount)
        {
            return null;
        }
        
        return GridHolder.Grid[rowIndex, columnIndex].GetComponent<GridElement>();
    }
}
