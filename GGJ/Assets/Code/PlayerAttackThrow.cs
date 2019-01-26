using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackThrow : MonoBehaviour
{
    public Transform ThrowingPoint;
    public GameObject projectile;
    private GameObject currentProjectile;
    private bool canAttack = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && canAttack && !GetComponent<Animator>().GetCurrentAnimatorStateInfo( 0 ).IsName( "JumpStart" ) && !GetComponent<Animator>().GetCurrentAnimatorStateInfo( 0 ).IsName( "Jump" ) && !GetComponent<Animator>().GetCurrentAnimatorStateInfo( 0 ).IsName( "JumpEnd" ) )
        {
            canAttack = false;
            GetComponent<PlayerMovement>().slowPlayerTemporarily();
            GetComponent<Animator>().SetTrigger("Fire1");
            StartCoroutine("delayedAttack");
        }

    }

    IEnumerator delayedAttack()
    {
        yield return new WaitForSeconds(0.4f);
        currentProjectile = Instantiate(projectile, ThrowingPoint.position, Quaternion.identity);
        var throwingAttack = currentProjectile.GetComponent<ThrowingAttack>();
        Vector3 aimVector = FindObjectOfType<CameraOrbit>().transform.position - Camera.main.transform.position;
        Vector3 aimingModifier = new Vector3(0, 3, 0);
        throwingAttack.perform(1000, aimVector + aimingModifier, 0);
        yield return new WaitForSeconds(1.3f);
        canAttack = true;

    }
}