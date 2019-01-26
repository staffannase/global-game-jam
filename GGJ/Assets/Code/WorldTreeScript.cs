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
    public List<GameObject> Leaves;
    public GameObject Trunk;
    public GameObject LeafPrefab;
    public Color SickColourTrunk;
    public Color RevivedColourTrunk;
    private bool GrowingLeaf;
    public float growthSpeed;

    public bool test;

    void Start () {
        state = TreeState.Lifeless;
        Leaves = new List<GameObject>();
	}

    private void Update()
    {
        if (test)
            StartCoroutine(AddLifeToTree());
    }

    public IEnumerator AddLifeToTree()
    {
        if (state == TreeState.Lifeless)
        {
            foreach(Transform trans in LeafPlaces)
            {
                var leaf = Instantiate(LeafPrefab, trans.transform.position, Quaternion.identity);
                Leaves.Add(leaf);
                GrowingLeaf = true;

                while (GrowingLeaf)
                {
                    leaf.transform.localScale *= Time.deltaTime * growthSpeed;

                    if (leaf.transform.localScale.x > 3f)
                        GrowingLeaf = false;
                }
            }

            state = TreeState.Sick;
            yield break;
        }
        else if (state == TreeState.Sick)
        {
            foreach (GameObject leaf in Leaves)
            {
                GrowingLeaf = true;

                while (GrowingLeaf)
                {
                    leaf.transform.localScale *= Time.deltaTime * growthSpeed;
                    // make it greeeeeeeeeeen

                    if (leaf.transform.localScale.x > 5f)
                        GrowingLeaf = false;
                }
            }

            state = TreeState.Revived;
            yield break;
        }
    }
}