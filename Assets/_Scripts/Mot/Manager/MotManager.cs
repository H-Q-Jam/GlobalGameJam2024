using System;
using System.Collections.Generic;
using UnityEngine;

public class MotManager : MonoBehaviour
{
    public static Mot equipe1;
    public static Mot equipe2;

    //ReadOnly dans MotManager_Editor
    [SerializeField] private List<Mot> mots = new ();
    //ReadOnly dans MotManager_Editor
    [SerializeField] private List<Lettre> lettres = new ();
    //ReadOnly dans MotManager_Editor
    [SerializeField] private List<Zone> zones = new ();

    private static readonly Dictionary<Zone, Lettre> zonesToLettre = new ();
    private static readonly Dictionary<Lettre, Mot> lettreToMot = new ();

    private void Awake()
    {
        Mot[] newMots = FindObjectsOfType<Mot>();
        for (int i = 0; i < newMots.Length; i++)
        {
            Mot newMot = newMots[i];
            mots.Add(newMot);
            switch (newMot.Equipe)
            {
                case TeamWhoCanGrab.Equipe1:
                    equipe1 = newMot;
                    break;
                case TeamWhoCanGrab.Equipe2:
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

    public static bool GetLettreFromZone(Zone zone, out Lettre lettre)
    {
        return zonesToLettre.TryGetValue(zone, out lettre);
    }
    public static bool GetMotFromLettre(Lettre lettre, out Mot mot)
    {
        return lettreToMot.TryGetValue(lettre, out mot);
    }
    public static bool GetMotFromZone(Zone zone, out Mot mot)
    {
        mot = null;
        if (GetLettreFromZone(zone, out Lettre lettre))
        {
            return GetMotFromLettre(lettre, out mot);
        }
        return false;
    }
}
