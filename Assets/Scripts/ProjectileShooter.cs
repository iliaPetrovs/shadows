using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectile;
    public Camera cam;
    public Transform firePoint;
    public AudioClip sfxProjectileThrow;
    public float projectileSpeed = 300f;
    public int projectileAmount = 10;


    private Vector3 target;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && GetComponent<ThirdPersonMovement>().isAiming)
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            target = hit.point;
        }
        else
        {
            target = ray.GetPoint(1000);
        }

        if (projectileAmount > 0)
        {
            InstantiateProjectile();
            projectileAmount -= 1;
        }
    }

    void InstantiateProjectile()
    {
        //firePoint.LookAt(target);
        PlaySound();
        GameObject projectileTransform = Instantiate(projectile, firePoint.position, firePoint.rotation);
        //projectileTransform.GetComponent<Rigidbody>().velocity = (target - firePoint.position).normalized * projectileSpeed;
        projectileTransform.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, projectileSpeed));
    }

    void PlaySound()
    {
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(sfxProjectileThrow);
    }
}
