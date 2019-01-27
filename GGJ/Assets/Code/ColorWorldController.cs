using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ColorWorldController : MonoBehaviour
{
    [SerializeField] PostProcessVolume volume;
    [SerializeField] float speed;

    private float grayTarget = 1f;

    private bool shouldChangeColor = false;
    private float error = 0.01f;

    void Update () {
        if (shouldChangeColor)
        {
            volume.weight = Mathf.Lerp(volume.weight, grayTarget, Time.deltaTime * speed);
            shouldChangeColor = Mathf.Abs(volume.weight - grayTarget) <= error;
        }
    }

    public void ChangeColor(float subtractGreyness)
    {
        grayTarget -= subtractGreyness;
        shouldChangeColor = true;
    }
}
