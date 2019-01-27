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

    private bool GameWon;
    private ColorWorldController wc;

    void Start ()
    {
        wc = FindObjectOfType<ColorWorldController>();
	}
	
	void Update ()
    {
        if (GameWon)
        {
            //GameWin
        }
    }

    private void CheckForFinishedWays()
    {
        if(WayOneEnemies.Count < 1)
        {
            WorldTree.finishedLevel++;
            wc.ChangeColor(0.2f);
        }
        else if (WayTwoEnemies.Count < 1)
        {
            WorldTree.finishedLevel++;
            wc.ChangeColor(0.2f);
        }
        else if (WayThreeEnemies.Count < 1)
        {
            WorldTree.finishedLevel++;
            wc.ChangeColor(0.2f);
        }
        else if (WayFourEnemies.Count < 1)
        {
            WorldTree.finishedLevel ++;
            wc.ChangeColor(0.2f);
        }
        else if (WayFiveEnemies.Count < 1)
        {
            WorldTree.finishedLevel++;
            wc.ChangeColor(0.2f);
        }
        if (WorldTree.finishedLevel==5)
        {
            GameWon = true;
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