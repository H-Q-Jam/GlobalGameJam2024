using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerRigidbodyMovement : MonoBehaviour
{

    [SerializeField] private PlayerGrab pg;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask layerGround;
    
    public float speed = 60f;
    
    public float Speed
    {
        get
        {
            float newSpeed = speed;
            if (pg != null)
            {
                if (pg.fournitureInHand != null)
                {
                    newSpeed = speed * Mathf.Clamp(50 / pg.fournitureInHand.Rb.mass, 0.5f, 1);
                }
            }
            return newSpeed;
        }
    }

    public float jumpForce = 50f;
    [SerializeField] private float maxVelocity = 100f;
    [SerializeField] private float rotationSpeed = 100f;
    public float RotationSpeed
    {
        get
        {
            float newRotationSpeed = rotationSpeed;
            if (pg != null)
            {
                if (pg.fournitureInHand != null)
                {
                    newRotationSpeed = rotationSpeed * Mathf.Clamp(10 / pg.fournitureInHand.Rb.mass, 0.1f, 1);
                }
            }
            return newRotationSpeed;
        }
    }

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
        direction = new Vector3(x, 0, z);
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
        var targetVelocity = direction * Speed * Time.fixedDeltaTime;

        if(isRunning)
        {
            targetVelocity *= runningMuliplicator;
        }

        rb.velocity = new Vector3(targetVelocity.x,rb.velocity.y,targetVelocity.z);

        if (new Vector3(direction.x, 0, direction.z) != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RotationSpeed * Time.fixedDeltaTime);
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
            Vector3 accel = vecGravity * fallMultiplier * Time.fixedDeltaTime;
            rb.velocity += accel;
        }
    }

    private void CheckIsGrounded()
    { 
        if(Physics.Raycast(transform.position, new Vector3 (0,-1,0), out RaycastHit hit, groundDistance,layerGround))
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
