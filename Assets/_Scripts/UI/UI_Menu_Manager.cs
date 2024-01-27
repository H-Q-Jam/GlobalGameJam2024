using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Menu_Manager : MonoBehaviour
{

    [SerializeField] private LevelManager levelManager;
    private GameMode gameMode;


    //panel pour la gestion de l'ui


    [SerializeField] private GameObject panel_Menu;
    [SerializeField] private GameObject panel_Option;
    [SerializeField] private GameObject panel_Play;
    [SerializeField] private GameObject header;
    [SerializeField] private GameObject panel_InGame;
    [SerializeField] private GameObject panel_ScoreEnd;
    [SerializeField] private GameObject panel_InProgress;

    [SerializeField] private TMP_InputField gameSessionID;

    private void Awake()
    {
        UI_StartSetup();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
       
    }

    public void UI_StartSetup()
    {
        panel_Menu.SetActive(true);
        panel_Option.SetActive(false);
        header.SetActive(false);
        panel_Play.SetActive(false);
        panel_InGame.SetActive(false);  
        panel_ScoreEnd.SetActive(false);
        panel_InProgress.SetActive(false);
    }

    public void OnPlay()
    {
 

    }

    public void OnHostGame()
    {
        Debug.Log("Host");
        SetGameMode(GameMode.Host);

    }
    public void OnJoinGame()
    {
        Debug.Log("Join");
        SetGameMode(GameMode.Client);

    }

    public void OnSetting()
    {
        gameSessionID.text = "";
        panel_Menu.SetActive(false);
        panel_Play.SetActive(false);
        header.SetActive(true);
        panel_Option.SetActive(true);
    }

    /// <summary>
    /// fonction qui lance la partie en fonction du game mode
    /// Host = cr�e la room avec l'id ecrit
    /// Join = rejoin la room avec l'id ecrit
    /// </summary>
    public void OnEnterGame()
    {
        Debug.Log(gameSessionID.text);
        gameSessionID.text = "";
       // StartGame(gameMode,)
        BackToMenu();
    }

    private void SetGameMode(GameMode gamemode)
    {
        Debug.Log("SetGameMode");
        gameMode = gamemode;

        if (panel_Menu.activeSelf)
        {
            header.SetActive(true);
            panel_Play.SetActive(true);
            panel_Menu.SetActive(false);
            
        }

    }


    public void BackToMenu()
    {
        gameSessionID.text = "";
        panel_Menu.SetActive(true);
        panel_Option.SetActive(false);
        panel_Play.SetActive(false);
        header.SetActive(false);

    }

}
