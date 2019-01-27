using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour {

    float lerpTime = 5.5f;
    float currentLerpTime;
    float startFov;
    float endFov;

    public GameObject renderTexture;
    Color startColor;
    Color endColor;

    // Use this for initialization
    void Start() {
        startFov = 60f;
        endFov = 80f;

        /*startColor = renderTexture.GetComponent<Renderer>().material.color;
        startColor = new Color( renderTexture.GetComponent<Renderer>().material.color.r, renderTexture.GetComponent<Renderer>().material.color.g, renderTexture.GetComponent<Renderer>().material.color.b, 140);
        */
    }

    // Update is called once per frame
    void Update() {
        //increment timer once per frame
        currentLerpTime += Time.deltaTime;
        if ( currentLerpTime > lerpTime ) {
            currentLerpTime = lerpTime;
        }

        //lerp!
        float perc = currentLerpTime / lerpTime;
        this.GetComponent<Camera>().fieldOfView = Mathf.Lerp( startFov, endFov, perc );
        //renderTexture.GetComponent<Renderer>().material.color = Color.Lerp( startColor, endColor, perc );
        if ( Mathf.Approximately( perc, 1 ) ) {
            float t = startFov;
            startFov = endFov;
            endFov = t;

            //Color c = startColor;
            //startColor = endColor;
            //endColor = c;

            currentLerpTime = 0f;
        }
    }
}
