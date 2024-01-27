using UnityEngine;

public partial class Zone : MonoBehaviour
{
    [SerializeField] protected Color normalColor;
    [SerializeField] protected Color valideColor;
    [SerializeField, ReadOnly] protected MeshRenderer meshRenderer;
    [SerializeField, ReadOnly] protected Material material;
    
    public void ChangeColor(bool isValid)
    {
        material.color = isValid ? valideColor : normalColor;
    }
    public void ChangeColorValid()
    {
        ChangeColor(true);
    }
    public void ChangeColorNormal()
    {
        ChangeColor(false);
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
}
