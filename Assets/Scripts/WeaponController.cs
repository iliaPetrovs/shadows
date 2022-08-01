using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject weapon;
    public AudioClip sfxSwordSlash;
    public bool canAttack = true;
    public float attackCooldown = 1.0f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !transform.parent.GetComponent<ThirdPersonMovement>().isAiming)
        {
            if (canAttack)
            {
                WeaponAttack();
            }
        }
    }

    private void WeaponAttack()
    {
        canAttack = false;
        Animator anim = weapon.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(sfxSwordSlash);
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown() {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
