using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour {

    public GameObject fruitPrefab;
    public Transform[] points;

    public void SpawnFruit()
    {
        Vector3 spawnPos = points[Random.Range(0, points.Length)].position;
        var fruit = Instantiate(fruitPrefab, spawnPos, Quaternion.identity);
    }
}