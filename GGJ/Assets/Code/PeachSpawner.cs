using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeachSpawner : MonoBehaviour {

    public float SpawnTimer { get; set; }
    public GameObject Peach;
    public bool Growing { get; set; }
    GameObject Fruit;
    // Use this for initialization
    void Start () {
        SpawnTimer = Time.fixedTime + 10;
        Growing = false;
        Fruit = null;
	}

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime > SpawnTimer)
        {
            SpawnTimer = float.MaxValue;
            Fruit = Instantiate(Peach, gameObject.transform.position, Quaternion.identity);
            Fruit.transform.position = gameObject.transform.position;
            Growing = true;
        }
        if (Growing && Fruit.transform.localScale.sqrMagnitude < Vector3.one.sqrMagnitude * 0.01)
        {
            Fruit.transform.localScale += Vector3.one * Time.deltaTime * (float)0.05;
            if (Fruit.transform.localScale.sqrMagnitude >= Vector3.one.sqrMagnitude * 0.05)
                Growing = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Fruit")
        {
            SpawnTimer = Time.fixedTime + 10;
        }
    }
}