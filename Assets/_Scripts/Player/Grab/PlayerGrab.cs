using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] private GrabHand _grabHand;
    [SerializeField] public Rigidbody _rightHand;
    [SerializeField] public Rigidbody _leftHand;
    [SerializeField] public FournitureManager fournitureInHand;
    
    
    [SerializeField] private CharacterJoint upperArmR;
    [SerializeField] private CharacterJoint upperArmL;
    
    // Update is called once per frame
    void Update()
    {
       HandleGrabInput();
    }

    private bool GrabOrUnGrab;
    private bool test;
    public void HandleGrabInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            test = false;
            GrabOrUnGrab = !GrabOrUnGrab;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            /*if (GrabOrUnGrab && !test)
            {
                test = true;
                ArmsUngrab();
            }
            else if (!test)
            {
                test = true;
                ArmsGrab();
            }*/
            
            Debug.Log("Grab");
            if (_grabHand._interactableGrab != null)
            {
                _grabHand._interactableGrab = _grabHand._interactableGrab.Interact(this);
            }
            else if (_grabHand._interactableDetected != null)
            {
                _grabHand._interactableGrab = _grabHand._interactableDetected.Interact(this);
            }

            if (_grabHand._interactableGrab != null)
            {
                ArmsGrab();
            }
            else
            {
                ArmsUngrab();
            }
        }
    }

    private void ArmsGrab()
    {
        SoftJointLimit highTwistLimit, lowTwistLimit, swing1Limit;

        #region UpperRightArm
        upperArmR.axis = new Vector3(-0.5f,-1,0);
                
        highTwistLimit = upperArmR.highTwistLimit;
        highTwistLimit.limit = 80;
        upperArmR.highTwistLimit = highTwistLimit;
                    
        lowTwistLimit = upperArmR.lowTwistLimit;
        lowTwistLimit.limit = 75;
        upperArmR.lowTwistLimit = lowTwistLimit;
                    
        swing1Limit = upperArmR.swing1Limit;
        swing1Limit.limit = 30;
        upperArmR.swing1Limit = swing1Limit;
        #endregion

        #region UpperLeftArm
        upperArmL.axis = new Vector3(0.5f,-1,0);
                    
        highTwistLimit = upperArmL.highTwistLimit;
        highTwistLimit.limit = -75;
        upperArmL.highTwistLimit = highTwistLimit;
                    
        lowTwistLimit = upperArmL.lowTwistLimit;
        lowTwistLimit.limit = -80;
        upperArmL.lowTwistLimit = lowTwistLimit;
                    
        swing1Limit = upperArmL.swing1Limit;
        swing1Limit.limit = 30;
        upperArmL.swing1Limit = swing1Limit;
        #endregion
    }
    
    private void ArmsUngrab()
    {
        SoftJointLimit highTwistLimit, lowTwistLimit, swing1Limit;

        #region UpperRightArm
        upperArmR.axis = new Vector3(0,-1,0);
                
        highTwistLimit = upperArmR.highTwistLimit;
        highTwistLimit.limit = 10;
        upperArmR.highTwistLimit = highTwistLimit;
                    
        lowTwistLimit = upperArmR.lowTwistLimit;
        lowTwistLimit.limit = -70;
        upperArmR.lowTwistLimit = lowTwistLimit;
                    
        swing1Limit = upperArmR.swing1Limit;
        swing1Limit.limit = 50;
        upperArmR.swing1Limit = swing1Limit;
        #endregion

        #region UpperLeftArm
        upperArmL.axis = new Vector3(0,-1,0);
                    
        highTwistLimit = upperArmL.highTwistLimit;
        highTwistLimit.limit = 10;
        upperArmL.highTwistLimit = highTwistLimit;
                    
        lowTwistLimit = upperArmL.lowTwistLimit;
        lowTwistLimit.limit = -70;
        upperArmL.lowTwistLimit = lowTwistLimit;
                    
        swing1Limit = upperArmL.swing1Limit;
        swing1Limit.limit = 50;
        upperArmL.swing1Limit = swing1Limit;
        #endregion
    }
}
