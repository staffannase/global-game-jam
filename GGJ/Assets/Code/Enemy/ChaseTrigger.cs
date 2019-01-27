using UnityEngine;

public class ChaseTrigger : MonoBehaviour
{

    public EnemyController enemy;

    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            enemy.Chase(c.gameObject);
        }
    }

    void OnTriggerExit (Collider c) {
        if (c.CompareTag ("Player") && enemy.state != StateOfEnemy.Idle) {
            enemy.StopChase();
        }
    }
}
