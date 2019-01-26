using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackThrow : MonoBehaviour
{
    public Transform ThrowingPoint;
    private InventoryController inventory;
    private PlayerMovement movement;
    private Animator animator;
    public GameObject projectile;
    private GameObject currentProjectile;
    private bool canAttack = true;

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
        if (Input.GetButtonDown("Fire1") && canAttack && !GetComponent<Animator>().GetBool("IsJumping") ) 
        {
            canAttack = false;
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
                default:
                    canAttack = true;
                    break;
            }
            
        }
    }

    IEnumerator grapeAttack()
    {
        yield return new WaitForSeconds(0.4f);
        currentProjectile = Instantiate(projectile, ThrowingPoint.position, Quaternion.identity);
        var throwingAttack = currentProjectile.GetComponent<ThrowingAttack>();
        Vector3 aimVector = FindObjectOfType<CameraOrbit>().transform.position - Camera.main.transform.position;
        Vector3 aimingModifier = new Vector3(0, 3, 0);
        throwingAttack.perform(1000, aimVector + aimingModifier);
        yield return new WaitForSeconds(1.3f);
        canAttack = true;

    }

    IEnumerator peachAttack()
    {
        yield return new WaitForSeconds(0.4f);
        currentProjectile = Instantiate(projectile, ThrowingPoint.position, Quaternion.identity);
        var throwingAttack = currentProjectile.GetComponent<ThrowingAttack>();
        Vector3 aimVector = FindObjectOfType<CameraOrbit>().transform.position - Camera.main.transform.position;
        Vector3 aimingModifier = new Vector3(0, 3, 0);
        throwingAttack.perform(1000, aimVector + aimingModifier);
        yield return new WaitForSeconds(1.3f);
        canAttack = true;

    }
}