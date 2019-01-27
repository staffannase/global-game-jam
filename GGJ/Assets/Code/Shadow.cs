using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour {

    private Vector3 targetPosition;
    private Vector3 targetScale;
    private Quaternion targetRotation;

    protected GameObject TargetedObject;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Debug.DrawRay( transform.position, Vector3.down );
        if ( Physics.Raycast( transform.position, Vector3.down, out hit ) ) {
            Debug.Log( hit.collider.gameObject.name );
            transform.position = new Vector3     (hit.point.x, hit.point.y, hit.point.z);
            //transform.position = transform.InverseTransformVector( transform.position );
        }
	}
}
