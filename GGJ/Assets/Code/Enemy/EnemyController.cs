using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum StateOfEnemy {
    Patrol,
    Chase,
    Idle,
    Friend
}

public class EnemyController : MonoBehaviour {

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    private StateOfEnemy state = StateOfEnemy.Patrol;
    public Animator animator;
    private float minDistance = 2f;

    private float nextAttackTime;
    private float timeBetweenAttack = 2f;

    private GameObject targetToChase;

    void Start () {
        agent = GetComponent<NavMeshAgent> ();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        //animator = GetComponent<Animator> ();
        animator.SetBool ("Move", true);

        GotoNextPoint ();
    }


    void GotoNextPoint () {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update () {
        switch (state) {
            case StateOfEnemy.Patrol:
                if (!agent.pathPending && IsDistanceToTargetIfEnough())
                    GotoNextPoint ();
                break;
            case StateOfEnemy.Chase:
                if (IsDistanceToTargetIfEnough ()) {
                    animator.SetBool ("Move", false);
                    TryToAttack ();
                }
                else
                {
                    agent.destination = targetToChase.transform.position;
                }
                break;
            case StateOfEnemy.Idle:
                animator.SetBool ("Move", false);
                break;
            default:
                break;
        }
    }

    public void Chase(GameObject obj)
    {
        targetToChase = obj;
        agent.autoBraking = true;
        state = StateOfEnemy.Chase;
    }

    public void StopChase () {
        state = StateOfEnemy.Patrol;
        GotoNextPoint ();
    }

    void TryToAttack () {
        if (nextAttackTime <= Time.time) {
            //Debug.Log ("Attack");
            animator.SetTrigger ("Attack");
            targetToChase.SendMessage ("GetDamage");
            nextAttackTime = Time.time + timeBetweenAttack;
        }
    }

    bool IsDistanceToTargetIfEnough () {
        return agent.remainingDistance < minDistance;
    }
}