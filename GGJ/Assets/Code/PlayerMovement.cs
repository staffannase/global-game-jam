using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour {

    //Public Variables
    public float movementSpeed = 5f;


    //Private Variables
    Rigidbody rb;
    bool isGrounded;
    Vector3 movement = Vector3.zero;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    private void Update() {
        //Check grounded state;
        Debug.DrawRay( transform.position, Vector3.down * 1.2f, Color.red );
        if ( Physics.Raycast( transform.position, Vector3.down, 1.2f ) ) {
            Debug.Log( "Grounded" );
            isGrounded = true;
        } else {
            Debug.Log( "Not Grounded" );
            isGrounded = true;
        }

        //Get Input
        float horizontal = Input.GetAxis( "Horizontal" );

        //Calculate movement direction vector
        movement = new Vector3( horizontal, 0, 0 );

        //Add Speed
        movement *= movementSpeed;
    }

    // Update is called once per frame
    void FixedUpdate () {
        //Move!
        Debug.Log( "Final Movement Vector: " + movement.ToString() );
        rb.MovePosition(transform.position + movement * Time.deltaTime );
	}
}
