using UnityEngine;

public class SineUpDownAnimation : AnimationComponent<LeftPolicy,RightPolicy,UpPolicy,DownPolicy>
{
    [SerializeField] private float amplitude;
    
    private float index;
    
    private void LateUpdate()
    {
        if (Triggered)
        {
            index += Time.deltaTime;
            float y = amplitude * Mathf.Sin (AnimSpeed*index);
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
