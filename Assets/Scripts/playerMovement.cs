using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float jumpForce = 600.0f;
    public float maxGroundDistance = 1.5f;
    private bool isGrounded;
    public bool allowDoubleJump = true;
    private int amountOfJumps = 2;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private KeyCode forward = KeyCode.W;
    [SerializeField]
    private KeyCode back = KeyCode.S;
    [SerializeField]
    private KeyCode left = KeyCode.A;
    [SerializeField]
    private KeyCode right = KeyCode.D;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float maxSpeed;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, maxGroundDistance);
        if (isGrounded == true)
        {
            amountOfJumps = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(forward))
        {
            rb.AddForce(transform.forward * moveSpeed);
        }
        if (Input.GetKey(back))
        {
            rb.AddForce(-transform.forward * moveSpeed / 2);
        }
        if (Input.GetKey(left))
        {
            rb.AddForce(-transform.right * moveSpeed / 4);
        }
        if (Input.GetKey(right))
        {
            rb.AddForce(transform.right * moveSpeed / 4);
        }

        if (Input.GetKeyDown("space"))
        {

            if (isGrounded)
            {
                // turn on the cursor
                GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, jumpForce, 0.0f));
                amountOfJumps = 2;
            }
            else if (amountOfJumps < 2 && allowDoubleJump)
            {
                // turn on the cursor
                GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, jumpForce, 0.0f));
                amountOfJumps = 2;
            }

        }
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

    }
}
}
