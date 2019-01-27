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

    public StateOfEnemy state = StateOfEnemy.Patrol;
    public Animator animator;
    private float minDistance = 2f;

    private float nextAttackTime;
    private float timeBetweenAttack = 2f;

    private GameObject targetToChase;
    private Transform currentTarget;

    private Vector3[] pointsToGoHome;
    private int indexOfGoingHome = 0;

    public string correctAmmoName;

    public AudioClip[] regularSounds;

    private AudioSource audioSource;

    void Start () {
        agent = GetComponent<NavMeshAgent> ();
        audioSource = GetComponent<AudioSource>();

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
        if (!audioSource.isPlaying)
        {
            audioSource.clip = regularSounds[Random.Range(0,regularSounds.Length)];
            audioSource.Play();
        }

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
            case StateOfEnemy.Friend:
                animator.SetBool("Move", false);
                FindnextHomeTarget();
                MoveTowardsCurrentTarget(10);
                break;
            default:
                break;
        }
    }

    public void FindHome()
    {
        agent.enabled = false;

        state = StateOfEnemy.Friend;
        GameObject go = new GameObject();
        currentTarget = go.transform;
        pointsToGoHome = new Vector3[2];
        pointsToGoHome[0] = transform.position + (Vector3.up * 50);
        var wt = GameObject.FindGameObjectWithTag("ReturnSpawnPoint");
        pointsToGoHome[1] = wt.transform.position;// + new Vector3(5 + Random.value * 10, 0, 5 + Random.value * 10);
        currentTarget.position = pointsToGoHome[0];
    }

    private void FindnextHomeTarget()
    {
        if (IsDistanceToTargetIfEnoughOld())
        {
            if (indexOfGoingHome == 0)
            {
                indexOfGoingHome++;
                currentTarget.position = pointsToGoHome[indexOfGoingHome];
                FindObjectOfType<WorldcolourController>().RemoveMeFromList(gameObject);
            }
            else
            {
                state = StateOfEnemy.Idle;
            }
        }
    }

    void OnCollisionEnter (Collision coll)
    {
        //Debug.Log(coll.transform.name);
        if (coll.gameObject.name.Contains (correctAmmoName)) {
            //Debug.Log ("MakeFriend " + name);
            MakeFriend ();
            if (agent.enabled)
            {
                agent.enabled = false;
            }

            Destroy (coll.gameObject);
        }
    }

    public void Chase(GameObject obj)
    {
        targetToChase = obj;
        agent.autoBraking = true;
        state = StateOfEnemy.Chase;
    }

    public void StopChase () {
        if (state != StateOfEnemy.Friend)
        {
            state = StateOfEnemy.Patrol;
            GotoNextPoint();
        }
    }

    public void MakeFriend () {
        gameObject.layer = 12;
        GetComponentInChildren<SpriteRenderer>().gameObject.layer = 12;
        state = StateOfEnemy.Friend;
        FindHome();
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

    private bool IsDistanceToTargetIfEnoughOld()
    {
        if (currentTarget == null)
            return false;
        return (transform.position - currentTarget.position).sqrMagnitude <= minDistance;
    }

    private void MoveTowardsCurrentTarget(int relativeSpeed = 1)
    {
        if (currentTarget != null)
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, Time.deltaTime  * relativeSpeed);
    }
}