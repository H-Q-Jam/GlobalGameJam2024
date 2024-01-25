using System.Collections.Generic;
using UnityEngine;

public partial class MotManager : MonoBehaviour
{
    [SerializeField] private Mot equipe1;
    [SerializeField] private Mot equipe2;

    //ReadOnly dans MotManager_Editor
    [SerializeField] private List<Mot> mots = new ();
    //ReadOnly dans MotManager_Editor
    [SerializeField] private List<Lettre> lettres = new ();
    //ReadOnly dans MotManager_Editor
    [SerializeField] private List<Zone> zones = new ();

    private Dictionary<Zone, Lettre> zonesToLettre = new ();
    private Dictionary<Lettre, Mot> lettreToMot = new ();

    public bool GetLettreFromZone(Zone zone, out Lettre lettre)
    {
        return zonesToLettre.TryGetValue(zone, out lettre);
    }
    public bool GetMotFromLettre(Lettre lettre, out Mot mot)
    {
        return lettreToMot.TryGetValue(lettre, out mot);
    }
}
