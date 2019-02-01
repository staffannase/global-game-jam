using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ColorWorldController : MonoBehaviour
{
    [SerializeField] PostProcessVolume volume;
    [SerializeField] float speed;

    public float grayTarget = 1f;

    private bool shouldChangeColor = false;
    private float error = 0.01f;

    public float LerpTimer;
    public float MaxLerpTime;
    private float perc;

    void Update () {
        //if (shouldChangeColor)
        //{
        //    volume.weight = Mathf.Lerp(volume.weight, grayTarget, Time.deltaTime * speed);
        //    shouldChangeColor = Mathf.Abs(volume.weight - grayTarget) <= error;
        //    //volume2.weight = Mathf.Lerp(volume2.weight, grayTarget, Time.deltaTime * speed);
        //    //shouldChangeColor = Mathf.Abs(volume2.weight - grayTarget) <= error;
        //}

        LerpGray();

        if (shouldChangeColor)
        {
            LerpTimer += Time.deltaTime;

            perc = LerpTimer / MaxLerpTime;

            if (LerpTimer > MaxLerpTime)
            {
                LerpTimer = MaxLerpTime;
                shouldChangeColor = false;
            }
        }

        // Cheat
        if (Input.GetKey(KeyCode.F1) && Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.B))
        {
            grayTarget = 0f;
            shouldChangeColor = true;
        }
    }

    public void ChangeColor(float subtractGreyness)
    {
        grayTarget = subtractGreyness;
        shouldChangeColor = true;
    }

    public void LerpGray()
    {
        volume.weight = Mathf.Lerp(1f, 0.8f, perc);
    }
}
