using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float GroundDrag; // attrito

    [Header("GroundCheck")] // check if the raycast reach the ground and go over the playerHeight (+0.2)
    public float playerHeight;
    public LayerMask Ground;
    bool youAreGrounded;

    [Header("Jump")]
    public float jumpForce;
    public float jumpRefresh; // reste jump timing
    public float airMult;
    bool readyToJump;

    [Header("keyManager")]
    public KeyCode jumpKey = KeyCode.Space;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;// freeze the rotation completely
    }
    private void FixedUpdate()
    {
        MovePlayer(); // rigidbpdy action
    }

    private void Update()
    {
        // grounded check 
        youAreGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 20000000f, Ground);// from the position of the player, check, going down(vector3.down)

        MyInputs();
        SpeedControl();

        rb.drag = true ? (rb.drag = GroundDrag) : (rb.drag = 0);
    }
    private void MyInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); // save into a variable the inputs
        verticalInput = Input.GetAxisRaw("Vertical");

        // when jump 
        if(Input.GetKey(jumpKey) && readyToJump && youAreGrounded)
        {
            readyToJump = false; // resetting the jump
            Jump();// jump now 

            Invoke(nameof(ResetJump), jumpRefresh); // this allow to continuosly jump over the time, which, allow you to do it 
        }
    }

    private void MovePlayer()
    {
        
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        // set the move direction following the vertical input and the orientation move (forward to go straight(1) and back(-1))
        // set the move direction following the horizontal orientation where you re looking ( right and left)


        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); // apply a force for everyDirection
    }

    private void SpeedControl() // to do 
    {
        Vector3 speedControlled = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // speed control
      if (speedControlled.magnitude > moveSpeed) // if the max speed will be overflow
        {
            Vector3 limitedVelocity = speedControlled.normalized* moveSpeed;
            rb.velocity = new Vector3(speedControlled.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        // reset y velocity and be sure that y = 0 before jump;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        // check if you are on the ground before jump;
        if (youAreGrounded)
        rb.AddForce(transform.up *jumpForce, ForceMode.Impulse); // we need one force.up

        else if (!youAreGrounded)
            rb.AddForce(transform.up * jumpForce * airMult, ForceMode.Impulse); // manage the player in the middle air with air mult;

    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
