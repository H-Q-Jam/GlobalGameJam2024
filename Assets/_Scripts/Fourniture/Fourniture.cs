using System;
using UnityEngine;

public class Fourniture : MonoBehaviour, IInteractable
{
    [SerializeField, ReadOnly] protected FournitureManager fournitureManager;
    
    [SerializeField, ReadOnly] public bool onZone;

    public IInteractable interactable => this;

    TeamWhoCanGrab IInteractable.WhoCanGrab() => fournitureManager.WhoCanGrab;
    
    void IInteractable.Interact()
    {
        
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
