using System.Collections.Generic;
using Zenject;

public abstract class AnimationPropagationPolicy
{
    [Inject] private GridHolder gridHolder;
    public GridHolder GridHolder => gridHolder;

    public abstract IEnumerable<IEnumerable<GridElement>> GetNext(AnimationComponent animationComponent);
}
