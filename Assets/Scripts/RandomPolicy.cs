using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

public class RandomPolicy : AnimationPropagationPolicy
{
    public override IEnumerable<IEnumerable<GridElement>> GetNext(AnimationComponent animationComponent)
    {
        List<AnimationComponent> objects = GridHolder.GetGridList().Select(p => p.GetComponent<AnimationComponent>()).ToList();
        
        objects.Remove(animationComponent);
        
        while (objects.Count > 0)
        {
            Random r = new Random();
            int randInd = r.Next(0, objects.Count);
            AnimationComponent next = objects[randInd];

            objects.Remove(next);
            IEnumerable<GridElement> output = new List<GridElement> { next.GetComponent<GridElement>() };
            yield return output;
        }
    }
}
