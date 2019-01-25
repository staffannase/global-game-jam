using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public enum state
    {
        free = 0,
        pickingup=1,
        taken = 2,
        pickedup=3
    };

    public GameObject player { get; set; }
    private GameObject hand { get; set; }
    public state _state { get; set; }
    public int Acceleration { get; set; }
                                       // Use this for initialization
    void Start () {
         player = GameObject.FindGameObjectWithTag("player");
        hand = GameObject.FindGameObjectWithTag("hand");
        _state = state.free;
        Acceleration = 0;
    }
	
	// Update is called once per frame
	void Update () {
        PickupObject();
	}
    public void PickupObject()
    {
        if ( _state == state.pickingup)
        {
            //transform.position += Vector3.forward * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, hand.transform.position, Time.deltaTime * Acceleration);
            Acceleration += 1;
            if (Vector3.Distance(transform.position, hand.transform.position) < 1)
            {
                _state = state.taken;
                Acceleration = 0;
            }
        }
        if (_state == state.taken)
        {
            transform.localScale += Vector3.one * Time.deltaTime * Acceleration;
            Acceleration += 2;
            if (transform.localScale.sqrMagnitude > (Vector3.one * 3).sqrMagnitude)
            {
                _state = state.pickedup;
            }
        }
        if (_state == state.pickedup)
        {
            Destroy(this.gameObject);
            ParticleSystem p= hand.GetComponent<ParticleSystem>();
            p.Play();
        }

    }

     private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("player") && _state == state.free)
        {
            _state = state.pickingup;
            var rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            Destroy(rb);
        }
    }
}
