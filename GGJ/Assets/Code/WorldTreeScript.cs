using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTreeScript : MonoBehaviour {

    public Transform[] LeafPlaces1;
    public Transform[] LeafPlaces2;
    public Transform[] LeafPlaces3;
    public Transform[] LeafPlaces4;
    public List<GameObject> Leaves;
    public List<GameObject> LeavesToGrow;
    public GameObject Trunk;
    public GameObject LeafPrefab;
    public Color DeadColourTrunk;
    public Color SickColourTrunk;
    public Color WoundedColourTrunk;
    public Color AlmostHealthyColourTrunk;
    public Color HealthyColourTrunk;
    private bool Growing;
    private int finishedLevel = 0;
    public Renderer rend;
    public float LerpTimer;
    public float MaxLerpTime;
    private float perc;

    private void Update()
    {
        GrowLeaves();
        ColourTree();

        if (Growing)
        {
            LerpTimer += Time.deltaTime;

            perc = LerpTimer / MaxLerpTime;

            if (LerpTimer > MaxLerpTime)
                LerpTimer = MaxLerpTime;
        }

        if (!Growing)
            LerpTimer = 0f;
        
    }

    private void ColourTree()
    {
        //if (Growing)
        //{
            if (finishedLevel == 1 && rend.material.color != SickColourTrunk)
            {
                rend.material.color = Color.Lerp(DeadColourTrunk, SickColourTrunk, perc);
            }
            if (finishedLevel == 2 && rend.material.color != WoundedColourTrunk)
            {
                rend.material.color = Color.Lerp(SickColourTrunk, WoundedColourTrunk, perc);
            }
            if (finishedLevel == 3 && rend.material.color != AlmostHealthyColourTrunk)
            {
                rend.material.color = Color.Lerp(WoundedColourTrunk, AlmostHealthyColourTrunk, perc);
            }
            if (finishedLevel == 4 && rend.material.color != HealthyColourTrunk)
            {
                rend.material.color = Color.Lerp(AlmostHealthyColourTrunk, HealthyColourTrunk, perc);
            }
        //}
    }

    public void AddLeaves1()
    {
        for (int i = 0; i < LeafPlaces1.Length; i++)
        {
            GameObject Leaf = Instantiate(LeafPrefab, LeafPlaces1[i].position, Quaternion.identity);
            LeavesToGrow.Add(Leaf);
            finishedLevel = 1;
            Growing = true;
        }
    }

    public void AddLeaves2()
    {
        for (int i = 0; i < LeafPlaces2.Length; i++)
        {
            GameObject Leaf = Instantiate(LeafPrefab, LeafPlaces2[i].position, Quaternion.identity);
            LeavesToGrow.Add(Leaf);
            finishedLevel = 2;
            Growing = true;
        }
    }

    public void AddLeaves3()
    {
        for (int i = 0; i < LeafPlaces3.Length; i++)
        {
            GameObject Leaf = Instantiate(LeafPrefab, LeafPlaces3[i].position, Quaternion.identity);
            LeavesToGrow.Add(Leaf);
            finishedLevel = 3;
            Growing = true;
        }
    }

    public void AddLeaves4()
    {
        for (int i = 0; i < LeafPlaces4.Length; i++)
        {
            GameObject Leaf = Instantiate(LeafPrefab, LeafPlaces4[i].position, Quaternion.identity);
            LeavesToGrow.Add(Leaf);
            finishedLevel = 4;
            Growing = true;
        }
    }

    public void GrowLeaves()
    {
        if (Growing)
        {
            for (int i = 0; i < LeavesToGrow.Count; i++)
            {
                GameObject leaf = LeavesToGrow[i];

                if (leaf.transform.localScale.sqrMagnitude < Vector3.one.sqrMagnitude * 3f)
                {
                    leaf.transform.localScale += Vector3.one * Time.deltaTime * 0.5f;

                    if (leaf.transform.localScale.sqrMagnitude >= Vector3.one.sqrMagnitude * 3f)
                    {
                        LeavesToGrow.Remove(leaf);
                        Leaves.Add(leaf);
                        Growing = false;
                    }
                }
            }
        }
    }
}