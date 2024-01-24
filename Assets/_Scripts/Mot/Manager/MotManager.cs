using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public partial class MotManager : MonoBehaviour
{
    [SerializeField] private Mot equipe1;
    [SerializeField] private Mot equipe2;

    [SerializeField, ReadOnly] private List<Mot> mots = new ();
    [SerializeField, ReadOnly] private List<Lettre> lettres = new ();
    [SerializeField, ReadOnly] private List<Zone> zones = new ();

    private Dictionary<Zone, Lettre> zonesToLettre;
    private Dictionary<Lettre, Mot> lettreToMot;
}
