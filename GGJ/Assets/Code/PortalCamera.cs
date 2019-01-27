using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour {

    float lerpTime = 5.5f;
    float currentLerpTime;
    float startFov;
    float endFov;

    // Use this for initialization
    void Start() {
        startFov = 60f;
        endFov = 80f;
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
        if ( Mathf.Approximately( perc, 1 ) ) {
            float t = startFov;
            startFov = endFov;
            endFov = t;
            currentLerpTime = 0f;
        }
    }
}
