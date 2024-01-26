using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerRigidbodyMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    public float speed = 60f;
    public float jumpForce = 50f;
    [SerializeField] private float jumpHeightMax = 2f;
    [SerializeField] private float maxVelocity = 100f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float runningMuliplicator;
    [SerializeField] private bool isRunning = false;

    [SerializeField] private float fallMultiplier;
    Vector3 vecGravity;

    [SerializeField] private bool isGrounded = true;
    [SerializeField] private float groundDistance = 0.2f;

    public Vector3 direction = Vector3.zero;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        vecGravity = new Vector3( 0, Physics.gravity.y, 0);
    }

    private void Update()
    {
        GetIsRunning();



        //jump here, in the fixedUpdate it doesn't works everytime
        CheckIsGrounded();
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else BeterJump();
    }


    private void FixedUpdate()
    {
        
        HandleMoveInput();
        Move();
       
    }

    private void HandleMoveInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        direction = new Vector3(x, rb.velocity.y, z);

    }


    private void GetIsRunning()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
 
            isRunning = true;
        }
        else
        {
            isRunning= false;   
        }
    }

    private void Move()
    {
        var targetVelocity = direction * speed * Time.fixedDeltaTime;


        if(isRunning)
        {
            targetVelocity *= runningMuliplicator;
        }

        rb.velocity = targetVelocity;


        if (new Vector3(direction.x, 0, direction.z) != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }

    }


    private void Jump()
    {

        rb.AddForce(new Vector3(0, jumpForce, 0),ForceMode.Impulse);
   
    }
    private void BeterJump()
    {
        if(rb.velocity.y < -2f)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.fixedDeltaTime;
        }
    }

    private void CheckIsGrounded()
    {
        int layermask = 0 << 12;


        RaycastHit hit;
     
        if(Physics.Raycast(transform.position, new Vector3 (0,-1,0), out hit, groundDistance))
        {
            Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * groundDistance, Color.green);
            isGrounded = true;
        }
        else
        {
            Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * groundDistance , Color.red);
            isGrounded = false;
        }
       
    }
   
}
