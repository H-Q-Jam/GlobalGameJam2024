using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class NetworkRunnerHandler : MonoBehaviour
{
    public NetworkRunner networkRunnerPrefab;

    NetworkRunner networkRunner;//reference du networkRunnerPrefab

    MainMenuUIHandler mainMenuUIHandler;
    // Start is called before the first frame update
    void Start()
    {
        mainMenuUIHandler = FindAnyObjectByType<MainMenuUIHandler>();



        networkRunner = Instantiate(networkRunnerPrefab);
        networkRunner.name = "Network runner";

        var clientTask = InitializeNetworkRunner(networkRunner, mainMenuUIHandler.gameMode, mainMenuUIHandler.gameSessionID.text.ToString(), NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);

        Debug.Log($"Server NetworkRunner started");
    }





    protected virtual Task InitializeNetworkRunner(NetworkRunner runner,GameMode gameMode, String roomName, NetAddress adress, SceneRef sceneRef, Action<NetworkRunner> initialized)
    {
        var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();


        if (sceneManager != null)
        {
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
     
            gameObject.AddComponent<NetworkPhysicsSimulation3D>();
        }

        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = adress,
            SessionName = roomName,
            Initialized = initialized,
            SceneManager = sceneManager

        }); 
    }

}
