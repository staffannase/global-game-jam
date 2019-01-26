using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfMapScript : MonoBehaviour {

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Deathwater"))
        {
            //Do some gameover stuff
            SceneManager.LoadScene("Level");
        }
    }

}