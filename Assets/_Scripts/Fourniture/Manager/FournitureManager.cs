using System;
using UnityEngine;

public class FournitureManager : MonoBehaviour
{
    [SerializeField] protected string nom;
    [SerializeField] protected float poids;
    [SerializeField] protected int taille;
    [SerializeField, ReadOnly] protected TeamWhoCanGrab _whoCanGrab;
    public TeamWhoCanGrab WhoCanGrab => _whoCanGrab;
    
    [SerializeField] protected Color normalColor;
    [SerializeField] protected Color valideColor;
    [SerializeField, ReadOnly] protected MeshRenderer meshRenderer;
    [SerializeField, ReadOnly] protected Material material;
    
    private void Awake()
    {
        SetMeshRenderer();

        ResetWhoCanGrab();
    }

    private void SetMeshRenderer()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            meshRenderer = GetComponentInParent<MeshRenderer>();
        }

        if (meshRenderer != null)
        {
            material = new Material(meshRenderer.material);
            meshRenderer.material = material;
        }
    }

    public void ResetWhoCanGrab()
    {
        _whoCanGrab = TeamWhoCanGrab.Equipe1;
        _whoCanGrab |= TeamWhoCanGrab.Equipe2;
    }
    
    public void ChangeWhoCanGrab(TeamWhoCanGrab whoCanGrab)
    {
        _whoCanGrab = whoCanGrab;
    }

    public void ChangeColor(bool isValid)
    {
        material.color = isValid ? valideColor : normalColor;
    }
}
