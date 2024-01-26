using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEditor.SceneManagement;
using System.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class NetworkRunnerHandler : MonoBehaviour
{
    public NetworkRunner networkRunnerPrefab;

    NetworkRunner networkRunner;//reference du networkRunnerPrefab


    // Start is called before the first frame update
    void Start()
    {
        networkRunner = Instantiate(networkRunnerPrefab);
        networkRunner.name = "Network runner";

        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);

        Debug.Log($"Server NetworkRunner started");
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner,GameMode gameMode, NetAddress adress, SceneRef sceneRef, Action<NetworkRunner> initialized)
    {
        var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();
        if (sceneManager != null)
        {
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = adress,
            SessionName = "TestRoom",
            Initialized = initialized,
            SceneManager = sceneManager

        }); 
    }

}
