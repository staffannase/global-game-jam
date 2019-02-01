using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class WorldTreeScript : MonoBehaviour {

    //public Transform[] LeafPlaces1;
    //public Transform[] LeafPlaces2;
    //public Transform[] LeafPlaces3;
    //public Transform[] LeafPlaces4;

    public GameObject[] Leaves1;
    public GameObject[] Leaves2;
    public GameObject[] Leaves3;
    public GameObject[] Leaves4;

    public List<GameObject> LeavesToGrow;
    public GameObject Trunk;
    public Color DeadColourTrunk;
    public Color SickColourTrunk;
    public Color WoundedColourTrunk;
    public Color AlmostHealthyColourTrunk;
    public Color HealthyColourTrunk;
    public bool Growing;
    public int finishedLevel = 0;
    public Renderer rend;
    public float LerpTimer;
    public float MaxLerpTime;
    private float perc;
    public float sizeChange;
    private bool colourChange;

    public GameObject WinCanvas;
    public GameObject Creditz;

    public GameObject TreeCamera;
    public SpriteRenderer Player;
    public GameObject PCamera1;
    public GameObject PCamera2;

    [SerializeField] PostProcessVolume volume;


    private void Update()
    {
        //AwakenLeaves();
        GrowLeaves();
        ColourTree();

        if (colourChange)
        {
            LerpTimer += Time.deltaTime;

            perc = LerpTimer / MaxLerpTime;

            if (LerpTimer > MaxLerpTime)
            {
                LerpTimer = MaxLerpTime;
                colourChange = false;
            }
                
        }
    }

    public IEnumerator ShowChangingTree()
    {
        TreeCamera.SetActive(true);
        Player.enabled = false;
        PCamera1.SetActive(false);
        PCamera2.SetActive(false);
        yield return new WaitForSeconds(5f);
        TreeCamera.SetActive(false);
        Player.enabled = true;
        PCamera1.SetActive(true);
        PCamera2.SetActive(true);
    }

    public IEnumerator GameWon()
    {
        WinCanvas.SetActive(true);
        TreeCamera.SetActive(true);
        Player.enabled = false;
        PCamera1.SetActive(false);
        PCamera2.SetActive(false);
        yield return new WaitForSeconds(15f);
        Creditz.SetActive(true);
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene("Intro");
    }


    private void ColourTree()
    {
        if (finishedLevel == 1 /*&& rend.material.color != SickColourTrunk*/)
        {
            rend.material.color = Color.Lerp(DeadColourTrunk, SickColourTrunk, perc);
            volume.weight = Mathf.Lerp(1f, 0.8f, perc);
        }
        else if (finishedLevel == 2 /*&& rend.material.color != WoundedColourTrunk*/)
        {
            rend.material.color = Color.Lerp(SickColourTrunk, WoundedColourTrunk, perc);
            volume.weight = Mathf.Lerp(0.8f, 0.6f, perc);
        }
        else if (finishedLevel == 3 /*&& rend.material.color != AlmostHealthyColourTrun*/)
        {
            rend.material.color = Color.Lerp(WoundedColourTrunk, AlmostHealthyColourTrunk, perc);
            volume.weight = Mathf.Lerp(0.6f, 0.4f, perc);
        }
        else if (finishedLevel == 4 /*&& rend.material.color != HealthyColourTrunk*/)
        {
            rend.material.color = Color.Lerp(AlmostHealthyColourTrunk, HealthyColourTrunk, perc);
            volume.weight = Mathf.Lerp(0.4f, 0.2f, perc);
        }
        else if (finishedLevel == 5 /*&& rend.material.color != HealthyColourTrunk*/)
        {
            rend.material.color = Color.Lerp(AlmostHealthyColourTrunk, HealthyColourTrunk, perc);
            volume.weight = Mathf.Lerp(0.2f, 0f, perc);
        }
    }

    public void AwakenLeaves()
    {
        if (finishedLevel == 1)
        {
            colourChange = true;
            for (int i = 0; i < Leaves1.Length; i++)
            {
                GameObject leaf = Leaves1[i];
                leaf.SetActive(true);
                LeavesToGrow.Add(leaf);
            }
            Growing = true;
        }
        else if (finishedLevel == 2)
        {
            colourChange = true;
            for (int i = 0; i < Leaves2.Length; i++)
            {
                GameObject leaf = Leaves2[i];
                leaf.SetActive(true);
                LeavesToGrow.Add(leaf);
            }
            Growing = true;
        }
        else if (finishedLevel == 3)
        {
            colourChange = true;
            for (int i = 0; i < Leaves3.Length; i++)
            {
                GameObject leaf = Leaves3[i];
                leaf.SetActive(true);
                LeavesToGrow.Add(leaf);
            }
            Growing = true;
        }
        else if (finishedLevel == 4)
        {
            colourChange = true;
            for (int i = 0; i < Leaves4.Length; i++)
            {
                GameObject leaf = Leaves4[i];
                leaf.SetActive(true);
                LeavesToGrow.Add(leaf);
            }
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

                if (leaf.transform.localScale.x < 0.0045f)
                {
                    leaf.transform.localScale += Vector3.one * Time.deltaTime * 0.00045f;

                    if (leaf.transform.localScale.sqrMagnitude >= 0.003f)
                    {
                        LeavesToGrow.Remove(leaf);
                        Growing = false;
                    }
                }
            }
        }
    }
}