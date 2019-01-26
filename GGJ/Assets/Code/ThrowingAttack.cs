﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingAttack : MonoBehaviour
{
    public GameObject subProjectile;
    public GameObject fragment;
    public GameObject explosion;
    private Rigidbody body;
    private int fragmentCount;
    private int splitCount;

    private int DEFAULT_FRAGMENT_COUNT = 30;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1") && splitCount > 0)
        {
            doExplosion();
            for (int i = 0; i < splitCount; i++)
            {
                initSubProjectile(i - splitCount / 2);
            }
            Destroy(gameObject);
        }
    }

    private void doExplosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    private void initSubProjectile(int xShatter)
    {
        Vector3 start = transform.position;
        start += transform.forward.normalized * 10;

        GameObject newProjectile = Instantiate(subProjectile, transform.position, transform.rotation);
        Vector3 newVelocity = new Vector3(
            body.velocity.x + xShatter,
            body.velocity.y + Random.Range(1f, 2f),
            body.velocity.z + Random.Range(-1.5f, -0.50f)
            );
        newProjectile.GetComponent<Rigidbody>().velocity = newVelocity;
    }

    public void perform(int speed, Vector3 direction, int fragmentCount, int splitCount)
    {
        this.fragmentCount = fragmentCount;
        this.splitCount = splitCount;
        body = GetComponent<Rigidbody>();
        body.AddForce(direction * Time.deltaTime * speed);
    }

    public void throwStick(int speed, Vector3 direction)
    {
        this.perform(speed, direction, DEFAULT_FRAGMENT_COUNT, 0);

    }

    public void OnCollisionEnter(Collision other)
    {

        for (int i = 0; i < fragmentCount; i++)
        {
            Instantiate(fragment, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}