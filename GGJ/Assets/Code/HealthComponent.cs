using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent damageEvent;

    [SerializeField]
    private int hp;

    public bool IsDead { get; set; }

    public void GetDamage()
    {
        if (!IsDead)
        {
            hp--;
            damageEvent.Invoke();
            CheckForDead ();
        }
    }

    void CheckForDead()
    {
        IsDead = hp == 0;
        if(IsDead)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
