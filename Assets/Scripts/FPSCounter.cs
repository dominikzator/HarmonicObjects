using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        var fps = 1f / Time.deltaTime;
        text.text = fps.ToString("F1") + " FPS";
    }
}
