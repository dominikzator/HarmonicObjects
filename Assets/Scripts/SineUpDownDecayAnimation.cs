using UnityEngine;

public class SineUpDownDecayAnimation : AnimationComponent<AllNeighboursPolicy>
{
    [SerializeField] private float amplitude;
    [SerializeField] private float decayTime;
    
    private float index;

    private float decayFactor = 1f;
    private float decayCurrentTime = 0f;
    
    private void LateUpdate()
    {
        if (Triggered)
        {
            decayCurrentTime += Time.deltaTime;
            decayFactor = Mathf.Clamp(1 - (decayCurrentTime / decayTime), 0f, 1f);
            if (decayFactor <= 0f)
            {
                Triggered = false;
                decayCurrentTime = 0f;
                index = 0f;
            }
            index += Time.deltaTime;
            float y = amplitude * Mathf.Sin (AnimSpeed*index) * decayFactor;
            Rigidbody.transform.position =
                new Vector3(Rigidbody.transform.position.x, y, Rigidbody.transform.position.z);
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
