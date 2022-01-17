using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;

    public float speed = 5.0F;
    public float jumpForce = 8.0F;
    public float energy = 100.0F;
    public float maxEnergy = 100.0F;
    public float mouseSensitivity = 5.0F;
    public float cameraPitch;
    public float gravity = 20.0F;

    public bool isSprinting = false;

    public CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;

    private float turner;
    private float looker;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
    }

    public void Move()
    {
        // Movement
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //moveDirection.Normalize();
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump") && energy >= 10)
            {
                moveDirection.y = jumpForce;
                energy -= 10;
            }
        }        
        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;
        // Apply charater movement
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void CameraMovement()
    {
        // X mouse rotation
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraPitch -= mouseDelta.y * mouseSensitivity;

        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);
    }

    public void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && controller.isGrounded && energy >= 0.1f)
        {
            isSprinting = true;
        } else
        {
            isSprinting = false;
        }
        if (isSprinting)
        {
            energy -= 0.02f;
            speed = 9f;
        }
        if (isSprinting == false && energy != maxEnergy)
        {
            speed = 5f;
            if (moveDirection.x == 0 || moveDirection.z == 0)
            {
                energy += 0.015f;
            } else
            {
                energy += 0.008f;
            }
            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }
        }
        if (energy <= 0)
        {
            energy = 0;
        }

    }

    private void Update()
    {
        Move();
        Sprint();
        CameraMovement();
    }
}
