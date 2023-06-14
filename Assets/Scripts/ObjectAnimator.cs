using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimator : MonoBehaviour
{
    [SerializeField] private float amplitudeY;
    [SerializeField] private float omegaY;
    
    private float sinArgument;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Update(){
        sinArgument += Time.deltaTime;
        float y = amplitudeY*Mathf.Sin (omegaY*sinArgument);
        transform.position = new Vector3(transform.position.x,y,transform.position.z);
    }
}
