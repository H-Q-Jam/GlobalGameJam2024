using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoloUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClickPlay()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
