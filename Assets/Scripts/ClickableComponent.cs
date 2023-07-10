using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ClickableComponent : MonoBehaviour, IPointerClickHandler, IClickable
{
    protected void Awake()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick " + gameObject.name);

        OnClick();
    }

    public virtual void OnClick()
    {
        Debug.Log("OnClick");
    }
}
