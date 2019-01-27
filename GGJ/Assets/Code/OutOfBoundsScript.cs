using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBoundsScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision col)
    {

            if (col.gameObject.CompareTag("Deathwater"))
            {
                //do game over stuff
                SceneManager.LoadScene("Level2");
            }
       
    }
}