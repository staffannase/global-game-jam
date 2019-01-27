using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public Transform portHere;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter( Collider other ) {
        if(other.tag == "Player") {
            other.transform.position = portHere.position;
            other.gameObject.GetComponent<HealthComponent>().SetHPFull();
        }
    }

}
