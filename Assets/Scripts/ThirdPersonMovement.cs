using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public GameObject aimCam;
    public GameObject crossHair;

    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    public float runSpeed = 20f;
    public float walkSpeed = 6f;
    public float aimSpeed = 1f;
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
        Aim();

        ToggleSprint();
        ToggleAim();

        aimCam.GetComponent<Animator>().SetBool("isAiming", isAiming);

        if (isAiming)
        {
            crossHair.SetActive(true);
        } else
        {
            crossHair.SetActive(false);
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothening);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * (isSprinting ? runSpeed : isAiming ? aimSpeed : walkSpeed) * Time.deltaTime);
        }

    }

    void ToggleSprint()
    {
        if (Input.GetButtonDown("Run"))
        {
            isSprinting = !isSprinting;
            isAiming = false;
        }
    }

    void ToggleAim()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isAiming = !isAiming;
            isSprinting = false;
        }
        if (isAiming)
        {

        }
    }

    void Aim()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
        transform.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);
    }
}
