﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingExample : MonoBehaviour {
    public Transform ThrowingPoint;
    public GameObject projectile;
    private GameObject currentProjectile;

	void Update () {
        
		if (Input.GetButtonDown("Fire1") && currentProjectile == null)
        {
            currentProjectile = Instantiate(projectile, ThrowingPoint.position, Quaternion.identity);
            var actions = currentProjectile.GetComponent<Throwing>();
            actions.throwStick(1550, new Vector3(1, 1, 0),10, 5);
        }
        
    }
}
