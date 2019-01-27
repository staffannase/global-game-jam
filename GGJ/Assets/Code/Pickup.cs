using System.Collections;
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
    public InventoryController Inventory;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        //Hand = GameObject.FindGameObjectWithTag("Hand");
        State = state.free;
        Acceleration = 0;
        Collider c = gameObject.GetComponent<Collider>();
        c.isTrigger = true;
        Inventory = Player.GetComponent<InventoryController>();
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
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Time.deltaTime * Acceleration * (float)0.1);
            var a = GetComponentInChildren<Animator>();
            if (a!=null)
                a.enabled = true;
            Acceleration += 1;
            if (Vector3.Distance(transform.position, Player.transform.position) < 0.001)
            {
                State = state.taken;
                Acceleration = 0;
            }
        }
        if (State == state.taken)
        {
            transform.localScale += Vector3.one * Time.deltaTime * Acceleration * (float)0.5;
            Acceleration += 2;

            var a = GetComponentInChildren<Animator>();
            if (a!=null)
                a.speed *= 2;
            if (transform.localScale.sqrMagnitude > (Vector3.one * 2.0f).sqrMagnitude)
            {
                State = state.pickedup;
            }
        }
        if (State == state.pickedup)
        {
            if (tag == "Grape")
                Inventory.addAmmo(InventoryController.ProjectileType.Grape);
            else if (tag == "Peach")
                Inventory.addAmmo(InventoryController.ProjectileType.Peach);
            else if (tag=="Banana")
                Inventory.addAmmo(InventoryController.ProjectileType.Banana);
            Destroy(gameObject);
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
