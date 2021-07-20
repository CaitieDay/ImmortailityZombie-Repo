using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float jumpForce = 600.0f;
    public float maxGroundDistance = 1.51f;
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
    [SerializeField]
    private float boostedJump;


    [SerializeField]
    public float sensitivity = 5.0f;
    [SerializeField]
    public float smoothing = 2.0f;
    // the chacter is the capsule
    public GameObject character;
    // get the incremental value of mouse moving
    private Vector2 mouseLook;
    // smooth the mouse moving
    private Vector2 smoothV;

    float initialAngle;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        initialAngle = character.transform.localEulerAngles.y;
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
            rb.AddForce(-transform.right * moveSpeed / 2);
        }
        if (Input.GetKey(right))
        {
            rb.AddForce(transform.right * moveSpeed / 2);
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



        // md is mosue delta
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        // the interpolated float result between the two float values
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        // incrementally add to the camera look
        mouseLook += smoothV;

        // vector3.right means the x-axis
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(initialAngle + mouseLook.x, Vector3.up);

    }
}

