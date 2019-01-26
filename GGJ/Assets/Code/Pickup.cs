﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class Pickup : MonoBehaviour
{

    public enum state
    {
        free = 0,
        pickingup = 1,
        taken = 2,
        pickedup = 3
    };

    public GameObject Player { get; set; }
    private GameObject Hand { get; set; }
    public state State { get; set; }
    public int Acceleration { get; set; }
    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Hand = GameObject.FindGameObjectWithTag("Hand");
        State = state.free;
        Acceleration = 0;
        Collider c = gameObject.GetComponent<Collider>();
        c.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        PickupObject();
    }
    public void PickupObject()
    {
        if (State == state.pickingup)
        {
            //transform.position += Vector3.forward * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Hand.transform.position, Time.deltaTime * Acceleration * (float)0.01);
            Acceleration += 1;
            if (Vector3.Distance(transform.position, Hand.transform.position) < 0.001)
            {
                State = state.taken;
                Acceleration = 0;
            }
        }
        if (State == state.taken)
        {
            transform.localScale += Vector3.one * Time.deltaTime * Acceleration * (float)0.01;
            Acceleration += 2;
            if (transform.localScale.sqrMagnitude > (Vector3.one * 0.3f).sqrMagnitude)
            {
                State = state.pickedup;
            }
        }
        if (State == state.pickedup)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && State == state.free)
        {
            State = state.pickingup;
        }
    }
}
