using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggyAlertZoneScript : MonoBehaviour {

    FroggyController parent;

    private void Start()
    {
        parent = transform.parent.GetComponent<FroggyController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (parent.state != StateOfEnemy.Chase && other.CompareTag("Player"))
        {
            parent.Chase();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (parent.state == StateOfEnemy.Chase && other.CompareTag("Player"))
        {
            parent.state = StateOfEnemy.Patrol;
        }
    }

}
