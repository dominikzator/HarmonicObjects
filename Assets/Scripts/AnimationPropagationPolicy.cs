using System.Collections.Generic;
using Zenject;

public abstract class AnimationPropagationPolicy
{
    [Inject] private GridHolder gridHolder;
    [Inject] private GlobalReferencesHolder globalReferencesHolder;
    public GridHolder GridHolder => gridHolder;
    public GlobalReferencesHolder GlobalReferencesHolder => globalReferencesHolder;

    public abstract IEnumerable<IEnumerable<GridElement>> GetNext(AnimationComponent animationComponent);
}
