using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTreeView : MonoBehaviour {

    Transform LookAtPoint;
    float speed = 100.666f;

	// Use this for initialization
	void Start () {
        LookAtPoint = transform.GetChild( 0 );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay( Collider other ) {
        if ( other.gameObject.CompareTag("Player")) {
            //other.transform.LookAt( LookAtPoint,  Vector3.up );

            Vector3 lTargetDir = LookAtPoint.position - other.transform.position;
            lTargetDir.y = 0.0f;
            other.transform.rotation = Quaternion.RotateTowards( other.transform.rotation, Quaternion.LookRotation( lTargetDir ), Time.deltaTime * speed );
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
