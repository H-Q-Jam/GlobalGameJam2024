using System;
using UnityEngine;


[Serializable]
public class Zone : MonoBehaviour
{
    private IInteractable _interactable;

    public IInteractable Interactable => _interactable;
}
