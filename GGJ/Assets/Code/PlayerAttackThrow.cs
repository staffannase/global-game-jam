﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackThrow : MonoBehaviour
{
    public Transform ThrowingPoint;
    public GameObject projectile;
    private GameObject currentProjectile;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && currentProjectile == null)
        {
            currentProjectile = Instantiate(projectile, ThrowingPoint.position, Quaternion.identity);
            var actions = currentProjectile.GetComponent<Throwing>();
            actions.throwStick(1550, new Vector3(0, 1, -1), 10, 5);
        }

    }
}