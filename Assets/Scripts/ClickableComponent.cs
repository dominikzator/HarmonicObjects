using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ClickableComponent : MonoBehaviour, IPointerClickHandler, IClickable
{
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }

    public virtual void OnClick()
    {
    }
}
