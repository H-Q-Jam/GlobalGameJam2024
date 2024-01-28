using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FournitureSpawner : NetworkBehaviour
{
     [SerializeField] private NetworkObject fourniture;


     private void OnEnable()
     {
         Spawner.OnServerReady += Spawn;
     }

     private void OnDisable()
     {
         Spawner.OnServerReady -= Spawn;
     }

     private void Spawn(NetworkRunner runner)
    {
        Spawner.OnServerReady -= Spawn;
        Debug.Log("Spawn Canape");
        NetworkObject networkObject = runner.Spawn(fourniture, transform.position);
        Debug.Log(networkObject);
    }
}
