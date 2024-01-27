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

    [SerializeField, ReadOnly] private Rigidbody rb;
    public Rigidbody Rb=> rb;
    [SerializeField] private ConfigurableJoint joint;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.mass = poids;
        }
        SetMeshRenderer();
        ResetWhoCanGrab();
    }

    public bool isJointed => joint.connectedBody != null;
    public void LinkJoint(Rigidbody rightHand)
    {
        
        joint.xMotion = ConfigurableJointMotion.Limited;
        joint.yMotion = ConfigurableJointMotion.Limited;
        joint.zMotion = ConfigurableJointMotion.Limited;
        joint.connectedBody = rightHand;
    }
    
    public void UnlinkJoint(Rigidbody rightHand)
    {
        if ( joint.connectedBody == rightHand)
        {
            joint.connectedBody = null;
            joint.xMotion = ConfigurableJointMotion.Free;
            joint.yMotion = ConfigurableJointMotion.Free;
            joint.zMotion = ConfigurableJointMotion.Free;
        }
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