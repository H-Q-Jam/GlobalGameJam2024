using System;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
public partial class Zone : MonoBehaviour
{
    [SerializeField] private Fourniture _fourniture;
    [SerializeField] private Collider _collider;

    public event Action OnOccupied; 
    public event Action OnUnoccupied; 

    public IInteractable Interactable => _fourniture;
    public bool isOccupied => Interactable != null;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        SetMeshRenderer();
    }

    private void OnEnable()
    {
        OnOccupied += ChangeColorValid;
        OnUnoccupied += ChangeColorNormal;
    }

    private void OnDisable()
    {
        OnOccupied -= ChangeColorValid;
        OnUnoccupied -= ChangeColorNormal;
    }

    private void OnTriggerStay(Collider other)
    {
        Fourniture newFourniture = other.GetComponent<Fourniture>();
        if (newFourniture != null)
        {
            if (_collider.bounds.Contains(other.bounds.center))
            {
                if (!newFourniture.onZone)
                {
                    if (MotManager.GetMotFromZone(this, out Mot mot))
                    {
                        if (_fourniture != newFourniture) InteractableOut();
                        _fourniture = newFourniture;
                        _fourniture.onZone = true;
                        _fourniture.ChangeColor(true);
                        _fourniture.interactable.ChangeWhoCanGrab(mot.Equipe);
                        
                        OnOccupied?.Invoke();
                    }
                }
            }
            else if (newFourniture.onZone)
            {
                if (newFourniture == _fourniture)
                {
                    InteractableOut();
                }
            }
        }

    }

    private void InteractableOut()
    {
        if (_fourniture != null)
        {
            _fourniture.onZone = false;
            _fourniture.ResetWhoCanGrab();
            _fourniture.ChangeColor(false);
            _fourniture = null;
            
            OnUnoccupied?.Invoke();
        }
    }
}
