using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TreeState
{
    Lifeless,
    Sick,
    Revived
}

public class WorldTreeScript : MonoBehaviour {

    public TreeState state;
    public Transform[] LeafPlaces;
    public GameObject Trunk;
    public GameObject LeafPrefab;
    public Color SickColourTrunk;
    public Color RevivedColourTrunk;

    void Start () {
        state = TreeState.Lifeless;
	}
	
	void Update () {
		
	}

    public void AddLife()
    {
        if (state == TreeState.Lifeless)
        {

            foreach (Transform t in LeafPlaces)
            {
                var leaf = Instantiate(LeafPrefab);
            }

        }
    }
}