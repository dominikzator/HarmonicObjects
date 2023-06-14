using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputReceiver : MonoBehaviour, IPointerClickHandler
{
    private Renderer renderer;
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");

        renderer.material.color = new Color(0f, 1f, 0f, 1f);
    }
}
