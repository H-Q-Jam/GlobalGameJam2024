using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    private NetworkRunner _runner;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private NetworkPrefabRef _playerPrefab;

    [SerializeField] private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();


    async void StartGame(GameMode mode)
    {
        // Create the Fusion runner and let it know that we will be providing user input
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        // Create the NetworkSceneInfo from the current scene
        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var sceneInfo = new NetworkSceneInfo();
        if (scene.IsValid)
        {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

        // Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "TestRoom",
            Scene = scene,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    private void OnGUI()
    {
        if (_runner == null)
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
            {
                StartGame(GameMode.Host);
            }
            if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
            {
                StartGame(GameMode.Client);
            }
        }
    }



    public void OnConnectedToServer(NetworkRunner runner)
    {
 
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
   
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
   
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
   
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {

    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
 
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

        if (Input.GetKey(KeyCode.W))
           {data.direction += Vector3.forward;}

        if (Input.GetKey(KeyCode.S))
            {data.direction += Vector3.back;}

        if (Input.GetKey(KeyCode.A))
            {data.direction += Vector3.left;}

        if (Input.GetKey(KeyCode.D))
            {data.direction += Vector3.right;}

        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {

    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {

    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {

            var randomPos = UnityEngine.Random.onUnitSphere * 2;
            randomPos.y = 0;
            var pos = _spawnPoint.position + randomPos;





            // Create a unique position for the player
            // Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.PlayerCount) * 3, 1, 0);
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, pos, Quaternion.identity, player);
            // Keep track of the player avatars for easy access
            _spawnedCharacters.Add(player, networkPlayerObject);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {

    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
 
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {

    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
 
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {

    }

}