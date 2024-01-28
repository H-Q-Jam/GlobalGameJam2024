using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Lettre : MonoBehaviour
{
    public event Action OnLetterComplete;
    public event Action OnLetterUncomplete;
    
    [SerializeField] private List<Zone> _zones;
    [SerializeField] public bool isComplete;

    public List<Zone> Zones => _zones;

    private void OnEnable()
    {
        foreach (Zone zone in _zones)
        {
            zone.OnOccupied += ZoneOccupied;
            zone.OnUnoccupied += ZoneUnoccupied;
        }
    }

    private void OnDisable()
    {
        foreach (Zone zone in _zones)
        {
            zone.OnOccupied -= ZoneOccupied;
            zone.OnUnoccupied -= ZoneUnoccupied;
        }
    }

    [SerializeField] private int nbZoneOccupied = 0;
    
    private void ZoneOccupied()
    {
        nbZoneOccupied = Mathf.Clamp(nbZoneOccupied+1,0,_zones.Count);
        if (nbZoneOccupied >= _zones.Count && !isComplete)
        {
            isComplete = true;
            OnLetterComplete?.Invoke();
        }
    }
    private void ZoneUnoccupied()
    {
        nbZoneOccupied = Mathf.Clamp(nbZoneOccupied-1,0,_zones.Count);
        if (nbZoneOccupied < _zones.Count && isComplete)
        {
            isComplete = false;
            OnLetterUncomplete?.Invoke();
        }
    }
}
