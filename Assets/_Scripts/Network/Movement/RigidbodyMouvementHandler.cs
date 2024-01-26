using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class RigidbodyMouvementHandler : NetworkBehaviour
{
    NetworkingRigidbodyController networkRigidbodyController;

    private void Awake()
    {
        networkRigidbodyController = GetComponent<NetworkingRigidbodyController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// only localy
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// update across the network
    /// input into data 
    /// the client send their input to the server and the server decide what happend
    /// and it's the server than move things
    /// </summary>
    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData networkInputData))
        {
            //Move
            Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right * networkInputData.movementInput.x;
            moveDirection.Normalize();

            networkRigidbodyController.Move(moveDirection);
            //Debug.Log("Calling Move from Network Rigidbody controller");
        }
    }
}
