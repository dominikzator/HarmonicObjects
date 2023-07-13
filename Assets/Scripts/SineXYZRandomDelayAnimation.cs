using UnityEngine;

public class SineXYZRandomDelayAnimation : AnimationComponent<AllNeighboursPolicy>
{
    [SerializeField] private float amplitude;
    [Tooltip("Endless if decayTime is set to 0")]
    [SerializeField] private float decayTime;
    [SerializeField] private bool includeXaxis;
    [SerializeField] private bool includeYaxis;
    [SerializeField] private bool includeZaxis;

    private float index;

    private Vector3 initialPos;

    private float xDelay, zDelay;
    
    private float decayFactor = 1f;
    private float decayCurrentTime = 0f;
    
    private void Awake()
    {
        base.Awake();
        initialPos = gameObject.transform.position;
        xDelay = Random.Range(0f, 1f);
        zDelay = Random.Range(0f, 1f);
    }

    private void LateUpdate()
    {
        if (Triggered)
        {
            decayCurrentTime += Time.deltaTime;
            decayFactor = (decayTime == 0f) ? 1f : Mathf.Clamp(1 - (decayCurrentTime / decayTime), 0f, 1f);
            if (decayFactor <= 0f)
            {
                Triggered = false;
                decayCurrentTime = 0f;
                index = 0f;
            }
            index += Time.deltaTime;
            float x = (includeXaxis) ? amplitude * Mathf.Sin (AnimSpeed*(index + xDelay)) * decayFactor : 0f;
            float y = (includeYaxis) ? amplitude * Mathf.Sin (AnimSpeed*index) * decayFactor : 0f;
            float z = (includeZaxis) ? amplitude * Mathf.Sin (AnimSpeed*(index + zDelay)) * decayFactor : 0f;
            Rigidbody.transform.localPosition = initialPos + new Vector3(x, y, z);
        }
    }
    public override void Animate()
    {
        if (Triggered)
        {
            return;
        }
        base.Animate();
    }
}
