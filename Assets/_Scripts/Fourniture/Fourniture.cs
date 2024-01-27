using System;
using UnityEngine;

public class Fourniture : MonoBehaviour, IInteractable
{
    [SerializeField, ReadOnly] protected FournitureManager fournitureManager;
    
    [SerializeField, ReadOnly] public bool onZone;

    public IInteractable interactable => this;

    TeamWhoCanGrab IInteractable.WhoCanGrab() => fournitureManager.WhoCanGrab;
    
    void IInteractable.Interact(PlayerGrab playerGrab)
    {
        if (fournitureManager.isJointed)
        {
            playerGrab.fournitureInHand = null;
            fournitureManager.UnlinkJoint(playerGrab._rightHand);
        }
        else
        {
            playerGrab.fournitureInHand = fournitureManager;
            fournitureManager.LinkJoint(playerGrab._rightHand);
        }
    }

    void IInteractable.ChangeWhoCanGrab(TeamWhoCanGrab whoCanGrab)
    {
        fournitureManager.ChangeWhoCanGrab(whoCanGrab);
    }
    
    public void ChangeColor(bool isValid)
    {
        fournitureManager.ChangeColor(isValid);
    }
    
    public void ResetWhoCanGrab()
    {
        fournitureManager.ResetWhoCanGrab();
    }

    private void Awake()
    {
        fournitureManager = GetComponentInParent<FournitureManager>();
    }
}
