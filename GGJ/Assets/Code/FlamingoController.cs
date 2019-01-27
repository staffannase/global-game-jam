using UnityEngine;

public class FlamingoController : MonoBehaviour
{

    [SerializeField] private Transform[] pointsToPatrol;

    [SerializeField] private float speed = 1f;

    [SerializeField] private Vector3[] pointsToGoHome;

    private Transform goHomeTaget;

    private StateOfEnemy state = StateOfEnemy.Patrol;

    private int indexOfPatroling = 0;
    private int indexOfGoingHome = 0;

    private Transform currentTarget;
    private Animator animator;

    private float minDistance = 0.5f;

    private float nextAttackTime;
    private float timeBetweenAttack = 2f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentTarget = pointsToPatrol[indexOfPatroling];
        animator.SetBool("Move", true);
  
    }

    private void Update()
    {
        switch (state)
        {
            case StateOfEnemy.Patrol:
                RotateTowardTarget();
                MoveTowardsCurrentTarget();
                CheckReachingPatrolPoint();
                break;
            case StateOfEnemy.Chase:
                if (IsDistanceToTargetIfEnough())
                {
                    animator.SetBool("Move", false);
                    TryToAttack();
                }
                else
                {
                    RotateTowardTarget();
                    transform.LookAt(currentTarget);
                    MoveTowardsCurrentTarget();
                }
                break;
            case StateOfEnemy.Idle:
                animator.SetBool("Move", false);
                break;
            case StateOfEnemy.Friend:
                animator.SetBool("Move", false);
                FindnextHomeTarget();
                MoveTowardsCurrentTarget(10);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state != StateOfEnemy.Chase && other.tag == "Player")
        {
            Chase(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (state == StateOfEnemy.Chase && other.transform == currentTarget)
        {
            Patrol();
        }
    }

    public void MakeFriend()
    {
        gameObject.layer = 10;
        state = StateOfEnemy.Friend;
        GetComponent<SphereCollider>().enabled = false;
        FindHome();
    }

    public void FindHome()
    {
        state = StateOfEnemy.Friend;
        GameObject go = new GameObject();
        currentTarget = go.transform;
        pointsToGoHome = new Vector3[2];
        pointsToGoHome[0] = transform.position + (Vector3.up * 50);
        var wt = GameObject.FindGameObjectWithTag("ReturnSpawnPoint");
        pointsToGoHome[1] = wt.transform.position + new Vector3(5+Random.value * 10,0 ,5+Random.value * 10);
        currentTarget.position = pointsToGoHome[0];
    }

    private void Chase(Transform target)
    {
        state = StateOfEnemy.Chase;
        currentTarget = target;
        transform.LookAt(currentTarget);
        animator.SetBool("Move", true);
    }

    private void Patrol()
    {
        currentTarget = pointsToPatrol[indexOfPatroling];
        transform.LookAt(currentTarget);
        state = StateOfEnemy.Patrol;
        indexOfPatroling = 0;
        animator.SetBool("Move", true);
    }

    private void MoveTowardsCurrentTarget(int relativeSpeed = 1)
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, Time.deltaTime * speed * relativeSpeed);
    }

    private void CheckReachingPatrolPoint()
    {
        if (IsDistanceToTargetIfEnough())
        {
            indexOfPatroling++;

            if (indexOfPatroling == pointsToPatrol.Length)
            {
                indexOfPatroling = 0;
            }

            currentTarget = pointsToPatrol[indexOfPatroling];
        }
    }

    private void FindnextHomeTarget()
    {
        if(IsDistanceToTargetIfEnough())
        {
            if (indexOfGoingHome == 0) {
                indexOfGoingHome++;
                currentTarget.position = pointsToGoHome[indexOfGoingHome];
            }
            else
            {
                state = StateOfEnemy.Idle;
            }
        }



    }

    private void RotateTowardTarget()
    {
        var targetRotation = Quaternion.LookRotation(currentTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }

    private void TryToAttack()
    {
        if (nextAttackTime <= Time.time)
        {
            //Debug.Log ("Attack");
            animator.SetTrigger("Attack");
            currentTarget.SendMessage("GetDamage");
            nextAttackTime = Time.time + timeBetweenAttack;
        }
    }

    private bool IsDistanceToTargetIfEnough()
    {
        return (transform.position - currentTarget.position).sqrMagnitude <= minDistance;
    }
}
