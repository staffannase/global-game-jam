using System.Collections;
using UnityEngine;

public class FroggyController : MonoBehaviour
{

    [SerializeField] private float speed = 20f;
    [SerializeField] private float maxAwayFromSpawn = 5f;
    [SerializeField] private float maxJumpRange = 5f;


    private StateOfEnemy state = StateOfEnemy.Patrol;

    private Rigidbody body;
    private Transform currentTarget;
    private Animator animator;

    private float minDistance = 0.5f;
    private Vector3 orgPos;

    private float nextAttackTime;
    private float timeBetweenAttack = 2f;

    private bool ongoingJump = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        animator.SetBool("Move", true);
        if (orgPos == Vector3.zero)
        {
            orgPos = transform.position;
        }
    }

    Vector3 getJumpingForce()
    {
        Vector3 pos = transform.position;
        Vector3 home = orgPos-pos;

        float x = Mathf.Abs(orgPos.x - pos.x) > maxAwayFromSpawn ? home.x: Random.Range(-maxJumpRange, maxJumpRange);
        float z = Mathf.Abs(orgPos.z - pos.z) > maxAwayFromSpawn ? home.z : Random.Range(-maxJumpRange, maxJumpRange);

        return new Vector3(x, pos.y+10, z) * speed;
    }

    IEnumerator jump()
    {
        animator.SetTrigger("froggyGoJump");
        body.AddForce(getJumpingForce());
        yield return new WaitForSeconds(Random.Range(4f,7f));
        ongoingJump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);

       if (collision.gameObject.CompareTag("Grape"))
        {
            MakeFriend();
        } else
        {
            animator.SetTrigger("froggyNoNoNoJump");
        }
        
    }


    public void MakeFriend()
    {
        gameObject.layer = 10;
        state = StateOfEnemy.Friend;
        FindHome();
    }

    public void FindHome()
    {

        GameObject treeCentre = GameObject.FindGameObjectWithTag("ReturnSpawnPoint");
        transform.position =  treeCentre.transform.position + new Vector3(5 + Random.value * 10, 2, 5 + Random.value * 10);
        orgPos = transform.position;
    }

    void Update()
    {


        if (ongoingJump)
        {
            return;
        }

        switch (state)
        {
            case StateOfEnemy.Patrol:
                ongoingJump = true;
                StartCoroutine("jump");
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
            default:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (state != StateOfEnemy.Chase && other.CompareTag("Player"))
        {
            Chase(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (state == StateOfEnemy.Chase && other.transform == currentTarget)
        {
            Patrol();
        }
    }

    void Chase(Transform target)
    {
        state = StateOfEnemy.Chase;
        currentTarget = target;
        transform.LookAt(currentTarget);
        animator.SetBool("Move", true);
    }

    void Patrol()
    {
        transform.LookAt(currentTarget);
        state = StateOfEnemy.Patrol;
        animator.SetBool("Move", true);
    }

    void MoveTowardsCurrentTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, Time.deltaTime * speed);
    }


    void RotateTowardTarget()
    {
        var targetRotation = Quaternion.LookRotation(currentTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }

    void TryToAttack()
    {
        if (nextAttackTime <= Time.time)
        {
            animator.SetTrigger("Attack");
            currentTarget.SendMessage("GetDamage");
            nextAttackTime = Time.time + timeBetweenAttack;
        }
    }

    bool IsDistanceToTargetIfEnough()
    {
        return (transform.position - currentTarget.position).sqrMagnitude <= minDistance;
    }
}
