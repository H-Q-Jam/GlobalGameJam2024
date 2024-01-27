using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] private GrabHand _grabHand;
    [SerializeField] public Rigidbody _rightHand;
    [SerializeField] public FournitureManager fournitureInHand;
    
    // Update is called once per frame
    void Update()
    {
        HandleGrabInput();
    }
    
    private void HandleGrabInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Grab");
            if (_grabHand._interactableGrab != null)
            {
                _grabHand._interactableGrab = _grabHand._interactableGrab.Interact(this);
            }
            else if (_grabHand._interactableDetected != null)
            {
                _grabHand._interactableGrab = _grabHand._interactableDetected.Interact(this);
            }
        }
    }
}
