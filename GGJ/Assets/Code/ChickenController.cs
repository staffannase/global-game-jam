using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour {

    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxAwayFromSpawn = 5f;
    [SerializeField] private float maxThrowRange = 5f;
    public GameObject egg;
    [SerializeField]  public Transform ThrowingPoint;

    public StateOfEnemy state = StateOfEnemy.Patrol;
    private Vector3 currentMoveTo;

    private Vector3 orgPos;

    private bool midAttack = false;

    private Rigidbody body;
    private GameObject player;
    private Animator animator;

    
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody>();
        if (orgPos == Vector3.zero)
        {
            orgPos = transform.position;
        }

    }

    IEnumerator assignNewTarget()
    {
        currentMoveTo = new Vector3(orgPos.x + Random.Range(-maxAwayFromSpawn, maxAwayFromSpawn), orgPos.y, orgPos.z + Random.Range(-maxAwayFromSpawn, maxAwayFromSpawn));
        yield return new WaitForSeconds(4);
        currentMoveTo = Vector3.zero;
        
    }
    
    void doTheChickenShuffle()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentMoveTo, Time.deltaTime);
    }

    public void Chase()
    {
        state = StateOfEnemy.Chase;
    }

    IEnumerator attackPlayer()
    {
        Debug.Log("Attacxking");
        midAttack = true;
        animator.SetTrigger("chickenAttacking");
        yield return new WaitForSeconds(0.5f);
        // THROW
        var current = Instantiate(egg, ThrowingPoint.position, Quaternion.identity);
        var EggAttack = current.GetComponent<EggAttack>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 aimAt = player.transform.position - transform.position;
        Vector3 aimingModifier = new Vector3(0, 1.5f, 0);
        EggAttack.perform(3200, aimAt + aimingModifier);
        yield return new WaitForSeconds(2);
        midAttack = false;
    }
	
    public void resumePatrol()
    {
        state = StateOfEnemy.Patrol;
    }




    // Update is called once per frame
    void Update () {
        if (currentMoveTo == Vector3.zero)
        {
            StartCoroutine("assignNewTarget");
        }
        switch (state)
        {
            case StateOfEnemy.Patrol:
                doTheChickenShuffle();                
                break;
            case StateOfEnemy.Chase:
                if (!midAttack)
                {
                    StartCoroutine("attackPlayer");
                }
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Banana") || collision.gameObject.CompareTag("Deathwater"))
        {
            MakeFriend();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            player.SendMessage("GetDamage");
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
        transform.position = treeCentre.transform.position + new Vector3(5 + Random.value * 10, 2, 5 + Random.value * 10);
        orgPos = transform.position;
    }
}
