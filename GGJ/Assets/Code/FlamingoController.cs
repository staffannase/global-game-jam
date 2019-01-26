using UnityEngine;

public enum StateOfEnemy {
    Patrol,
    Chase,
    Idle
}

public class FlamingoController : MonoBehaviour {

    [SerializeField] private Transform[] pointsToPatrol;

    [SerializeField] private float speed = 1f;

    private StateOfEnemy state = StateOfEnemy.Patrol;

    private int indexOfPatroling = 0;

    private Transform currentTarget;
    private Animator animator;

    private float minDistance = 0.5f;

    private float nextAttackTime;
    private float timeBetweenAttack = 2f;

    void Start ()
    {
        animator = GetComponent<Animator>();
        currentTarget = pointsToPatrol[indexOfPatroling];
        animator.SetBool ("Move", true);
    }

    void Update () {
        switch (state) {
            case StateOfEnemy.Patrol:
                RotateTowardTarget ();
                MoveTowardsCurrentTarget ();
                CheckReachingPatrolPoint ();
                break;
            case StateOfEnemy.Chase:
                if (IsDistanceToTargetIfEnough ()) {
                    animator.SetBool ("Move", false);
                    TryToAttack ();
                } else {
                    RotateTowardTarget ();
                    transform.LookAt (currentTarget);
                    MoveTowardsCurrentTarget ();
                }
                break;
            case StateOfEnemy.Idle:
                animator.SetBool("Move", false);
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter (Collider other) {
        if (state != StateOfEnemy.Chase && other.tag == "Player") {
            Chase (other.transform);
        }
    }

    void OnTriggerExit (Collider other) {
        if (state == StateOfEnemy.Chase && other.transform == currentTarget) {
            Patrol ();
        }
    }

    void Chase (Transform target) {
        state = StateOfEnemy.Chase;
        currentTarget = target;
        transform.LookAt (currentTarget);
        animator.SetBool ("Move", true);
    }

    void Patrol () {
        currentTarget = pointsToPatrol[indexOfPatroling];
        transform.LookAt (currentTarget);
        state = StateOfEnemy.Patrol;
        indexOfPatroling = 0;
        animator.SetBool ("Move", true);
    }

    void MoveTowardsCurrentTarget () {
        transform.position = Vector3.MoveTowards (transform.position, currentTarget.position, Time.deltaTime * speed);
    }

    void CheckReachingPatrolPoint () {
        if (IsDistanceToTargetIfEnough ()) {
            indexOfPatroling++;

            if (indexOfPatroling == pointsToPatrol.Length) {
                indexOfPatroling = 0;
            }

            currentTarget = pointsToPatrol[indexOfPatroling];
        }
    }

    void RotateTowardTarget () {
        var targetRotation = Quaternion.LookRotation (currentTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);
    }

    void TryToAttack () {
        if (nextAttackTime <= Time.time) {
            //Debug.Log ("Attack");
            animator.SetTrigger("Attack");
            currentTarget.SendMessage("GetDamage");
            nextAttackTime = Time.time + timeBetweenAttack;
        }
    }

    bool IsDistanceToTargetIfEnough () {
        return (transform.position - currentTarget.position).sqrMagnitude <= minDistance;
    }
}
