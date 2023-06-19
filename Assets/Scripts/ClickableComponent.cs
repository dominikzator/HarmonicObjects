using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public abstract class ClickableComponent : MonoBehaviour, IPointerClickHandler, IClickable
{
    private Renderer renderer;
    
    protected void Awake()
    {
        renderer = GetComponent<Renderer>();
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
