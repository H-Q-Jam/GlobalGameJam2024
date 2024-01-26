using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.Diagnostics;
public class Spawner : MonoBehaviour, INetworkRunnerCallbacks
{

    public NetworkPlayer playerPrefab;

    private Dictionary<PlayerRef, NetworkObject> spawnedPlayer = new Dictionary<PlayerRef, NetworkObject>();

    CharacterInputHandler characterInputHandler; // local player
    // Start is called before the first frame update
    void Start()
    {
        
    }



    public void OnConnectedToServer(NetworkRunner runner) { Debug.Log("OnConnectedToServer");}

    public void OnDisconnectedToServer(NetworkRunner runner) { Debug.Log("OnDisconnectedToServer"); }


    /// <summary>
    /// function than will spawn the player on join
    /// </summary>
    /// <param name="runner"></param>
    /// <param name="player"></param>
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    { 
        
        if(runner.IsServer)
        {
            Debug.Log("OnPlayerJoined we are the server. Spawning Player");
            NetworkObject np = runner.Spawn(playerPrefab, Utils.GetRandomSpawnPoint(), Quaternion.identity, player).gameObject.GetComponent<NetworkObject>();
            spawnedPlayer.Add(player, np);
        }
        else
        {
            Debug.Log("OnPlayerJoined");
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) 
    {
        Debug.Log("OnPlayerLeft");
        if (spawnedPlayer.TryGetValue(player, out NetworkObject obj))
        {
            runner.Despawn(obj);
            spawnedPlayer.Remove(player);
        }
      
    }


    /// <summary>
    /// here we get the input and need to send it to the host who will take care of this.
    /// </summary>
    /// <param name="runner"></param>
    /// <param name="input"></param>
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        //check if we get the input from the good player
        if (characterInputHandler == null && NetworkPlayer.Local != null)
        {
            characterInputHandler = NetworkPlayer.Local.GetComponent<CharacterInputHandler>();
        }


        if(characterInputHandler != null)
        {
            input.Set(characterInputHandler.GetNetworkInput()); 
        }
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { Debug.Log("OnShutDown");  }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { Debug.Log("OnConnectRequest"); }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { Debug.Log("OnConnectFailed"); }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }

    public void OnSceneLoadDone(NetworkRunner runner) { }

    public void OnSceneLoadStart(NetworkRunner runner) { }

    void INetworkRunnerCallbacks.OnDisconnectedFromServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }
}
