using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Camera cam;
    private bool collided = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && !collided)
        {
            collided = true;
            Destroy(gameObject);
        }
    }

    //void Update()
    //{
    //    Aim();
    //}

    //void Aim()
    //{
    //    Vector3 aimSpot = cam.transform.position;
    //    aimSpot += cam.transform.forward * 50.0f;
    //    transform.LookAt(aimSpot);
    //}
}
