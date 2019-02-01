using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldcolourController : MonoBehaviour {

    public List<GameObject> WayOneEnemies;
    public List<GameObject> WayTwoEnemies;
    public List<GameObject> WayThreeEnemies;
    public List<GameObject> WayFourEnemies;
    public List<GameObject> WayFiveEnemies;
    public WorldTreeScript WorldTree;

    public GameObject WayOneFence;
    public GameObject WayTwoFence;
    public GameObject WayThreeFence;
    public GameObject WayFourFence;
    public GameObject WayFiveFence;

    private ColorWorldController wc;
    private WorldTreeScript wt;

    void Start ()
    {
        wc = FindObjectOfType<ColorWorldController>();
        wt = FindObjectOfType<WorldTreeScript>();
	}
	

    private void CheckForFinishedWays()
    {
        if(WayOneEnemies.Count < 1 && !WayOneFence.activeSelf)
        {
            WorldTree.finishedLevel++;
            StartCoroutine(WorldTree.ShowChangingTree());
            WorldTree.AwakenLeaves();
            WayOneFence.SetActive(true);
            wc.ChangeColor((1f - (WorldTree.finishedLevel * 0.2f)));
        }
        else if (WayTwoEnemies.Count < 1 && !WayTwoFence.activeSelf)
        {
            WorldTree.finishedLevel++;
            StartCoroutine(WorldTree.ShowChangingTree());
            WorldTree.AwakenLeaves();
            WayTwoFence.SetActive(true);
            wc.ChangeColor((1f - (WorldTree.finishedLevel * 0.2f)));
        }
        else if (WayThreeEnemies.Count < 1 && !WayThreeFence.activeSelf)
        {
            WorldTree.finishedLevel++;
            StartCoroutine(WorldTree.ShowChangingTree());
            WorldTree.AwakenLeaves();
            WayThreeFence.SetActive(true);
            wc.ChangeColor((1f - (WorldTree.finishedLevel * 0.2f)));
        }
        else if (WayFourEnemies.Count < 1 && !WayFourFence.activeSelf)
        {
            WorldTree.finishedLevel ++;
            StartCoroutine(WorldTree.ShowChangingTree());
            WorldTree.AwakenLeaves();
            WayFourFence.SetActive(true);
            wc.ChangeColor((1f - (WorldTree.finishedLevel * 0.2f)));
        }
        else if (WayFiveEnemies.Count < 1 && !WayFiveFence.activeSelf)
        {
            WorldTree.finishedLevel++;
            StartCoroutine(WorldTree.ShowChangingTree());
            WayFiveFence.SetActive(true);
            wc.ChangeColor((1f - (WorldTree.finishedLevel * 0.2f)));
        }
        if (WorldTree.finishedLevel==5)
        {
            wc.ChangeColor((1f - (WorldTree.finishedLevel * 0.2f)));
            StartCoroutine(wt.GameWon());
        }
    }

    public void RemoveMeFromList(GameObject me)
    {
        if (WayOneEnemies.Contains(me))
        {
            WayOneEnemies.Remove(me);
        }
        else if (WayTwoEnemies.Contains(me))
        {
            WayTwoEnemies.Remove(me);
        }
        else if (WayThreeEnemies.Contains(me))
        {
            WayThreeEnemies.Remove(me);
        }
        else if (WayFourEnemies.Contains(me))
        {
            WayFourEnemies.Remove(me);
        }
        else if (WayFiveEnemies.Contains(me))
        {
            WayFiveEnemies.Remove(me);
        }

        CheckForFinishedWays();
    }
}