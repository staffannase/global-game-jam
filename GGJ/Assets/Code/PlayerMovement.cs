using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour {

    //Public Variables
    public float movementSpeed = 5f;

    //Private Variables
    Rigidbody rb;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //Get Input
        float horizontal = Input.GetAxis( "Horizontal" );

        //Calculate movement vector
        Vector3 movement = new Vector3( horizontal, 0, 0 );

        //Add Speed
        movement *= movementSpeed;

        //Move!
        rb.AddForce( movement );
	}
}
