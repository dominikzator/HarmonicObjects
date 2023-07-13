using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationVisitor
{
    private AnimationPropagationPolicy animationPropagationPolicy;
    private List<AnimationComponent> animationComponents;
    public List<AnimationComponent> AnimationComponents => animationComponents;
    
    public AnimationVisitor(AnimationPropagationPolicy policy, List<AnimationComponent> animationComponents)
    {
        this.animationPropagationPolicy = policy;
        this.animationComponents = animationComponents;
    }
    public IEnumerator StartAnimateAsync(AnimationComponent animationComponent)
    {
        animationComponent.Animate();
        foreach (var next in animationPropagationPolicy.GetNext(animationComponent))
        {
            yield return new WaitForSeconds(animationComponent.AnimDelay);
            
            foreach (var nextSingleElem in next.Where(p => !p.GetComponents<AnimationComponent>().First(q => q.GetType() == animationComponent.GetType()).Triggered))
            {
                var components = nextSingleElem.GetComponents<AnimationComponent>();
                var animComp = components.First(p => p.GetType() == animationComponent.GetType());
                animComp.Animate();
            }
        }

        yield return null;
    }
}
