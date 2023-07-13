using System.Collections.Generic;
using System.Linq;

public class OneByOnePolicy : AnimationPropagationPolicy
{
    public override IEnumerable<IEnumerable<GridElement>> GetNext(AnimationComponent animationComponent)
    {
        GridElement gridElement = animationComponent.GetComponent<GridElement>();
        
        List<AnimationComponent> objects = GridHolder.GetGridList().Select(p => p.GetComponent<AnimationComponent>()).ToList();

        int ind = objects.IndexOf(gridElement.GetComponent<AnimationComponent>());
        int nextInd = (ind + 1 >= objects.Count) ? 0 : ind + 1;
        
        List<AnimationComponent> objectsInOrder = objects.GetRange(nextInd, objects.Count - nextInd);

        if (nextInd - 1 >= 0)
        {
            objectsInOrder.AddRange(objects.GetRange(0, nextInd - 1));
        }
        objectsInOrder.Remove(animationComponent);
        
        foreach (var nextObj in objectsInOrder)
        {
            IEnumerable<GridElement> output = new List<GridElement> { nextObj.GetComponent<GridElement>() };
            yield return output;
        }
    }
}
