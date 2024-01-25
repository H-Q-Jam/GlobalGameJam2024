using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Mot : MonoBehaviour
{
    private int equipe;
    private List<Lettre> _lettres;

    public int Equipe => equipe;
    public List<Lettre> Lettres => _lettres;
}
