using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent deadEvent;

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
            deadEvent.Invoke();
    }
}
