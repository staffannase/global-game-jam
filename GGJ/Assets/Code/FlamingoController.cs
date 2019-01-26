using UnityEngine;

public enum StateOfEnemy
{
    Patrol,
    Chase,
    Idle
}

public class FlamingoController : MonoBehaviour
{

    [SerializeField] private Transform[] pointsToPatrol;

    [SerializeField] private float speed = 1f;

    private StateOfEnemy state = StateOfEnemy.Patrol;

    private int indexOfPatroling = 0;

    private Transform currentTarget;

    private float minDistance = 0.1f;

    void Start ()
    {
        currentTarget = pointsToPatrol[indexOfPatroling];
    }
	
	void Update ()
    {
        switch (state)
        {
            case StateOfEnemy.Patrol:
                RotateTowardTarget ();
                MoveTowardsCurrentTarget ();
                CheckReachingPatrolPoint ();
                break;
            case StateOfEnemy.Chase:
                RotateTowardTarget ();
                transform.LookAt (currentTarget);
                MoveTowardsCurrentTarget ();
                break;
            case StateOfEnemy.Idle:
                break;
            default:
                break;
        }
	}

    void OnTriggerEnter (Collider other) {
        if (state != StateOfEnemy.Chase && other.tag == "Player")
        {
            Chase(other.transform);
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.transform == currentTarget)
        {
            Patrol();
        }
    }

    void Chase(Transform target)
    {
        state = StateOfEnemy.Chase;
        currentTarget = target;
        transform.LookAt (currentTarget);
    }

    void Patrol()
    {
        currentTarget = pointsToPatrol[indexOfPatroling];
        transform.LookAt (currentTarget);
        state = StateOfEnemy.Patrol;
        indexOfPatroling = 0;
    }

    void MoveTowardsCurrentTarget()
    {
        transform.position = Vector3.MoveTowards (transform.position, currentTarget.position, Time.deltaTime * speed);
    }

    void CheckReachingPatrolPoint()
    {
        if ((transform.position-currentTarget.position).sqrMagnitude <= minDistance) {
            indexOfPatroling++;

            if (indexOfPatroling == pointsToPatrol.Length) {
                indexOfPatroling = 0;
            }

            currentTarget = pointsToPatrol[indexOfPatroling];
        }
    }

    void RotateTowardTarget()
    {
        var targetRotation = Quaternion.LookRotation(currentTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed* Time.deltaTime);
    }

}
