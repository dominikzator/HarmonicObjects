using DG.Tweening;
using UnityEngine;

public class ScaleAnimation : AnimationComponent<AllNeighboursPolicy>
{
    [SerializeField] private float scaleDownValue;
    [SerializeField] private float scaleUpValue;
    [SerializeField] private bool isInfinite;
    [SerializeField] private Ease scaleDownEase;
    [SerializeField] private Ease scaleUpEase;
    public override void Animate()
    {
        if (Triggered)
        {
            return;
        }
        base.Animate();
        ScaleAnim();
    }

    private Sequence ScaleAnim()
    {
        Sequence sequence = DOTween.Sequence();
        Tween scaleDownTween = gameObject.transform.DOScale(scaleDownValue, AnimSpeed).SetEase(scaleDownEase);
        Tween scaleUpTween = gameObject.transform.DOScale(scaleUpValue, AnimSpeed).SetEase(scaleUpEase);
        sequence.Append(scaleDownTween).Append(scaleUpTween);

        if (isInfinite)
        {
            sequence.SetLoops(-1);
        }

        return sequence;
    }
}
