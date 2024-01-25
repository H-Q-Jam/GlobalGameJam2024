using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Lettre : MonoBehaviour
{
    private List<Zone> _zoneses;

    public List<Zone> Zones => _zoneses;
}
