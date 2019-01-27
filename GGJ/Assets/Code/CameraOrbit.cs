using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour {

    //GET CAMERA TO ZOOM OUT AFTER ZOOMING IN WHEN HITTING AN OBJECT

    public Transform target;
    float distance = 3.5f;
    float xSpeed = 120.0f;
    float ySpeed = 120.0f;

    float yMinLimit = -20f;
    float yMaxLimit = 20f;

    float xMinLimit = -25f;
    float xMaxLimit = 25f;


    float distanceMin = 0.5f; 
    float distanceMax = 5f;

    float x = 0.0f;
    float y = 0.0f;
    float currentDistance;

    // Use this for initialization
    void Start() {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        currentDistance = distance;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate() {
        if ( target ) {
            x += Input.GetAxis("Mouse X") + Input.GetAxis( "HorizontalPan" ) * xSpeed * 0.02f;
            y -= Input.GetAxis( "Mouse Y" ) + Input.GetAxis( "VerticalPan" ) * ySpeed * 0.02f;

            y = ClampAngle( y, yMinLimit, yMaxLimit );
            x = ClampAngle( x, xMinLimit, xMaxLimit );

            Quaternion rotation = Quaternion.Euler( y, x, 0 );

            transform.localRotation = rotation;

        }

        if(Input.GetKeyDown(KeyCode.F3)) {
            Cursor.lockState = Cursor.visible ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !Cursor.visible;
        }
    }

    public static float ClampAngle( float angle, float min, float max ) {
        if ( angle < -360F )
            angle += 360F;
        if ( angle > 360F )
            angle -= 360F;
        return Mathf.Clamp( angle, min, max );
    }
}
