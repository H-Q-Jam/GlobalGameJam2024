using System;
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

    private readonly Dictionary<Zone, Lettre> zonesToLettre = new ();
    private readonly Dictionary<Lettre, Mot> lettreToMot = new ();

    private void Awake()
    {
        Mot[] newMots = FindObjectsOfType<Mot>();
        for (int i = 0; i < newMots.Length; i++)
        {
            Mot newMot = newMots[i];
            mots.Add(newMot);
            switch (newMot.Equipe)
            {
                case 1:
                    equipe1 = newMot;
                    break;
                case 2:
                    equipe2 = newMot;
                    break;
            }
            lettres.AddRange(newMot.Lettres);
            foreach (var newLettre in newMot.Lettres)
            {
                lettreToMot.TryAdd(newLettre, newMot);
                zones.AddRange(newLettre.Zones);
                foreach (var zone in newLettre.Zones)
                {
                    zonesToLettre.TryAdd(zone, newLettre);
                }
            }
        }
    }

    public bool GetLettreFromZone(Zone zone, out Lettre lettre)
    {
        return zonesToLettre.TryGetValue(zone, out lettre);
    }
    public bool GetMotFromLettre(Lettre lettre, out Mot mot)
    {
        return lettreToMot.TryGetValue(lettre, out mot);
    }
}
