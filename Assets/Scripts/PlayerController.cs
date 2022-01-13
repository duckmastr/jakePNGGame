using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5.0F;
    public float jumpForce = 8.0F;
    public float sensitivity = 5.0F;
    public float gravity = 20.0F;

    private Vector3 moveDirection = Vector3.zero;

    private float turner;
    private float looker;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Move()
    {
        // Movement
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
            
        // Look
        turner = Input.GetAxis("Mouse X") * sensitivity;
        looker = Input.GetAxis("Mouse Y") * sensitivity;
        if (turner != 0)
        {
            // Math to make character look left and right
            transform.eulerAngles += new Vector3(0, turner, 0);
        }
        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;
        // Apply charater movement
        controller.Move(moveDirection * Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
}
