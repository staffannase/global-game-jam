using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour {

    public GameObject fruitPrefab;
    public GameObject parent;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnFruit()
    {
        Vector3 spawnPos = parent.transform.position;
        spawnPos.x += Random.Range(-0.3f, 0.3f);
        spawnPos.z += Random.Range(-0.3f, 0.3f);
        var fruit = Instantiate(fruitPrefab, spawnPos, Quaternion.identity);
    }
}