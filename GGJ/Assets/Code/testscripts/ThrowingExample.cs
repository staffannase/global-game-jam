using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingExample : MonoBehaviour {
    public Transform ThrowingPoint;
    public GameObject projectile;

	void Update () {
        
		if (Input.GetButtonDown("Fire1"))
        {
            var stick = Instantiate(projectile, ThrowingPoint.position, Quaternion.identity);
            var comp = stick.GetComponent<Throwing>();
            comp.throwStick(1550, new Vector3(1, 1, 0),10, 5);
        }
        
    }
}
