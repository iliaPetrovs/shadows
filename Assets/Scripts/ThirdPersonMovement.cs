using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public GameObject aimCam;
    public GameObject mainCam;
    public GameObject crossHair;

    public float runSpeed = 20f;
    public float walkSpeed = 6f;
    public float turnSmoothening = 0.1f;
    public float turnSmoothVelocity;

    private bool canSprint = true;

    private bool isSprinting = false;
    public bool isAiming = false;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        ToggleSprint();
        ToggleAim();

        if (isAiming)
        {
            mainCam.SetActive(false);
            aimCam.SetActive(true);
            crossHair.SetActive(true);
        } else
        {
            mainCam.SetActive(true);
            aimCam.SetActive(false);
            crossHair.SetActive(false);
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothening);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * (isSprinting ? runSpeed : walkSpeed) * Time.deltaTime);
        }

    }

    void ToggleSprint()
    {
        if (Input.GetButtonDown("Run"))
        {
            isSprinting = !isSprinting;
        }
    }

    void ToggleAim()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isAiming = !isAiming;
        }
    }
}
