using System;
using UnityEngine;

public class GrabHand : MonoBehaviour
{
    [SerializeField] private TeamWhoCanGrab _whoCanGrab;
    public IInteractable _interactableGrab;
    public IInteractable _interactableDetected;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            if ( (interactable.WhoCanGrab() & _whoCanGrab) != 0)
            {
                Debug.Log("can grab");
                _interactableDetected = interactable;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            if ( _interactableDetected == interactable)
            {
                _interactableDetected = null;
            }
        }
    }
}
