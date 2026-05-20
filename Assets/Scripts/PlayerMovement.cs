using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{


    [Header("Movement")]
    public float moveSpeed; //make private later
    //public float walkSpeed;
    //public float sprintSpeed;

    public float groundDrag;


    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;
    
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsTheGround;
    bool grounded;
    public float groundCheckRadius = 0.2f;


    public Transform orientation;

    [Header("Animation")]
    public Animator animator;

    [Header("Debug (see raycast for grounded)")]
    public bool ToggleRayCast = false;

    float horitzontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void MyInput()
    {
        horitzontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // jumping
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false; // we set it to false so that we can't stack jumps
            Jump(); 

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horitzontalInput;
        bool isBunnyHopping = Input.GetKey(jumpKey);

        if (grounded)
        {
            if (isBunnyHopping)
            {
                // --- BHOP ---
                rb.linearDamping = 0; // No friction
                
                // Get current speed (ignoring jumping Y axis)
                Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

                //  SPEED LIMIT: Only add force if we are below the limit.
                if (flatVel.magnitude < moveSpeed * 1.2f) 
                {
                    // slowly add speed
                    rb.AddForce(moveDirection.normalized * moveSpeed * 2f, ForceMode.Force);
                }
            }
            else
            {
                // --- WALKING MODE (Snappy) ---
                // This logic stays the same as before
                rb.linearDamping = groundDrag; 
                Vector3 targetVelocity = moveDirection.normalized * moveSpeed;
                Vector3 velocityDiff = targetVelocity - new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                rb.AddForce(velocityDiff * 10f, ForceMode.Acceleration);
            }
        }
        else if (!grounded)
        {
            // --- AIR MOVEMENT ---
            rb.linearDamping = 0; 
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //limit the speed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);

        }
    }

    private void OnDrawGizmosSelected() // This is the debug for grounded.
    {
        // Only draw the gizmo if our debug toggle is checked
        if (ToggleRayCast == true) 
        {
            // Set the color based on whether we are grounded
            Gizmos.color = grounded ? Color.green : Color.red;

            // Draw a wire sphere at the player's position
            Gizmos.DrawWireSphere(transform.position + Vector3.down, groundCheckRadius);
        }
    }

    private void Jump()
    {
        // reset y velocity. (Jumps are the same height)
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        //ground checking with a raycast
        Vector3 groundcheckPos = transform.position + Vector3.down;
        grounded = Physics.CheckSphere(groundcheckPos, groundCheckRadius, whatIsTheGround);

        MyInput();

        //Handle the drag so we arent on ice skates
        if (grounded)
        {
            if(Input.GetKey(jumpKey) && readyToJump)
            {// if we are trying to bunny hop
                rb.linearDamping = 0;
            }
            else
            { 
                // We are just walking on the ground, so apply drag AND the speed limit.
                rb.linearDamping = groundDrag;
                //SpeedControl();
            }
        }
        else 
        {
            rb.linearDamping = 0;
        }// air drag 0

        if (animator != null)
        {
            // Get velocity ignoring Y (Up/Down)
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            
            // Send the speed to the Blend Tree
            animator.SetFloat("Forward", flatVel.magnitude);
        }

    }
    void FixedUpdate()
    {
        MovePlayer();
    }
}
