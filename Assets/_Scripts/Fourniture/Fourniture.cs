using UnityEngine;

public class Fourniture : MonoBehaviour, IInteractable
{
    protected string nom;
    protected float poids;
    protected int taille;
    protected (int,int) coordBase;
    protected bool[][] grille;
    
    void IInteractable.Interact()
    {
        
    }
}
