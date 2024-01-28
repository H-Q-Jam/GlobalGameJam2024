using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;

    bool isJumpPressed;
    bool isGrabPressed;

    int jump = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move Input
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = 1;
        }
        else
        {
            jump = 0;
        }



        if(Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }




    
        isGrabPressed = Input.GetKeyDown(KeyCode.E);
    }

    /// <summary>
    /// we collect the input at each frame and the network will collect it through this function
    /// </summary>
    /// <returns></returns>
    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        networkInputData.movementInput = moveInputVector;

        networkInputData.isJumpPressed = isJumpPressed;

        networkInputData.isGrabPressed = isGrabPressed;



        isJumpPressed = false;

        return networkInputData;
    }
}
