using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackThrow : MonoBehaviour
{
    public Transform ThrowingPoint;
    public GameObject peach;
    public GameObject grape;
    public GameObject grapeFragment;
    public GameObject banana;

    private InventoryController inventory;
    private PlayerMovement movement;
    private Animator animator;
    private bool ongoingAttack = false;

    // Use this for initialization
    void Start()
    {
        inventory = GetComponent<InventoryController>();
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")
            && !animator.GetBool("IsJumping")
            && inventory.hasAmmo()
            && !ongoingAttack
            ) 
        {
            ongoingAttack = true;
            movement.slowPlayerTemporarily();
            animator.SetTrigger("Fire1");
            inventory.popAmmo();
            switch (inventory.getAmmoType())
            {
                case InventoryController.ProjectileType.Grape:
                    StartCoroutine("grapeAttack");
                    break;
                case InventoryController.ProjectileType.Peach:
                    StartCoroutine("peachAttack");
                    break;
                case InventoryController.ProjectileType.Banana:
                    StartCoroutine("bananaAttack");
                    break;
                default:
                    ongoingAttack = false;
                    break;
            }
        }
    }

    IEnumerator grapeAttack()
    {
        yield return new WaitForSeconds(0.4f);
        var current = Instantiate(grape, ThrowingPoint.position, Quaternion.identity);
        var throwingAttack = current.GetComponent<ThrowingAttack>();
        Vector3 aimVector = FindObjectOfType<CameraOrbit>().transform.position - Camera.main.transform.position;
        Vector3 aimingModifier = new Vector3(0, 4, 0);
        throwingAttack.perform(500, aimVector + aimingModifier, 20, grapeFragment);
        yield return new WaitForSeconds(1.3f);
        ongoingAttack = false;
    }

    IEnumerator peachAttack()
    {
        yield return new WaitForSeconds(0.4f);
        var current = Instantiate(peach, ThrowingPoint.position, Quaternion.identity);
        var throwingAttack = current.GetComponent<ThrowingAttack>();
        Vector3 aimVector = FindObjectOfType<CameraOrbit>().transform.position - Camera.main.transform.position;
        Vector3 aimingModifier = new Vector3(0, 1.5f, 0);
        throwingAttack.perform(3200, aimVector + aimingModifier);
        yield return new WaitForSeconds(1.3f);
        ongoingAttack = false;
    }

    IEnumerator bananaAttack()
    {
        yield return new WaitForSeconds(0.4f);
        var current = Instantiate(banana, ThrowingPoint.position, Quaternion.identity);
        var throwingAttack = current.GetComponent<ThrowingAttack>();
        Vector3 aimVector = FindObjectOfType<CameraOrbit>().transform.position - Camera.main.transform.position;
        Vector3 aimingModifier = new Vector3(0, 1.5f, 0);
        throwingAttack.perform(3200, aimVector + aimingModifier);
        yield return new WaitForSeconds(1.3f);
        ongoingAttack = false;
    }
}