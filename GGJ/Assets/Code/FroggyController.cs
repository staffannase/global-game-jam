using System.Collections;
using UnityEngine;

public class FroggyController : MonoBehaviour
{

    [SerializeField] private float speed = 20f;
    [SerializeField] private float maxAwayFromSpawn = 5f;
    [SerializeField] private float maxJumpRange = 5f;

    // Alert range - same as alerting collider, dont mess with the zohan
    private float alertRange = 3f;
    public StateOfEnemy state = StateOfEnemy.Patrol;

    private Rigidbody body;
    private GameObject player;
    private Animator animator;

    private float minDistance = 0.5f;
    private Vector3 orgPos;

    private float nextAttackTime;
    private float timeBetweenAttack = 2f;

    private bool showWarFace = false;
    private bool ongoingJump = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody>();
        animator.SetBool("Move", true);

        if (orgPos == Vector3.zero)
        {
            orgPos = transform.position;
        }
    }

    Vector3 getPatrolJump()
    {
        Vector3 pos = transform.position;
        Vector3 home = orgPos-pos;

        float x = Mathf.Abs(orgPos.x - pos.x) > maxAwayFromSpawn ? home.x: Random.Range(-maxJumpRange, maxJumpRange);
        float z = Mathf.Abs(orgPos.z - pos.z) > maxAwayFromSpawn ? home.z : Random.Range(-maxJumpRange, maxJumpRange);

        return new Vector3(x, pos.y+10, z) * speed;
    }

    Vector3 getChaseJump()
    {
        Vector3 target = player.transform.position - transform.position;
        return new Vector3(target.x, transform.position.y + 10, target.z) * speed * 1.5f;
    }

    IEnumerator jump()
    {
        Debug.Log(state);
        if (showWarFace)
        {
            yield return new WaitForSeconds(1.5f);
        } else 
        {
            if (state == StateOfEnemy.Patrol)
            {
                animator.SetTrigger("froggyGoJump");
                body.AddForce(getPatrolJump());
                yield return new WaitForSeconds(Random.Range(4f, 7f));
            }
            else if (state == StateOfEnemy.Chase)
            {
                animator.SetTrigger("froggyGoJump");
                body.AddForce(getChaseJump());
                yield return new WaitForSeconds(Random.Range(3f, 4f));
            }
            else if (state == StateOfEnemy.Friend)
            {
                yield break;
            }
        }
        showWarFace = false;
        ongoingJump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grape") || collision.gameObject.CompareTag("Deathwater"))
        {
            MakeFriend();
        } else if (collision.gameObject.CompareTag("Player") && state != StateOfEnemy.Friend) {
            player.SendMessage("GetDamage");
        } else
        {
            animator.SetTrigger("froggyNoNoNoJump");
        }
        
    }

    public void MakeFriend()
    {
        gameObject.layer = 12;
        state = StateOfEnemy.Friend;
        FindHome();
    }

    public void FindHome()
    {
        FindObjectOfType<WorldcolourController>().RemoveMeFromList(gameObject);
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
            case StateOfEnemy.Friend:
                ongoingJump = false;
                break;
            case StateOfEnemy.Patrol:
                ongoingJump = true;
                StartCoroutine("jump");
                break;
            case StateOfEnemy.Chase:
                ongoingJump = true;
                StartCoroutine("jump");
                break;
            default:
                break;
        }
    }

    public void Chase()
    {
        showWarFace = true;
        animator.SetTrigger("froggyFullFrontalAllOutAttack");
        state = StateOfEnemy.Chase;
    }
}