using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    public UiEquipe Equipe1;
    public UiEquipe Equipe2;
    
    public CanvasGroup canvasGroup;
    
    private void OnEnable()
    {
        canvasGroup.alpha = 0;
        GameTimer.OnTimerOver += GameTimerOnTimerOver;
    }

    private void OnDisable()
    {
        GameTimer.OnTimerOver -= GameTimerOnTimerOver;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }

    private void GameTimerOnTimerOver()
    {
        canvasGroup.alpha = 1;
        Time.timeScale = 0;
        if (MotManager.equipe1 != null)
        {
            UpdateUi(MotManager.equipe1,Equipe1);
        }
        if (MotManager.equipe2 != null)
        {
            UpdateUi(MotManager.equipe2,Equipe2);
        }
    }

    private void UpdateUi(Mot mot, UiEquipe uiEquipe)
    {
        int pointMot = 0;
        int pointLettre = 0;
        int pointZone = 0;
        if (mot.isComplete)
        {
            pointMot += 3;
        }
        uiEquipe.pointMot.SetText(pointMot.ToString());

        foreach (Lettre lettre in mot.Lettres)
        {
            if (lettre.isComplete)
            {
                pointLettre++;
            }
            foreach (Zone zone in lettre.Zones)
            {
                if (zone.isOccupied)
                {
                    pointZone++;
                }
            }
        }
        uiEquipe.pointLettre.SetText(pointLettre.ToString());
        uiEquipe.pointCase.SetText(pointZone.ToString());
        uiEquipe.pointTotal.SetText((pointMot+pointLettre+pointZone).ToString());
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
[Serializable]
public struct UiEquipe
{
    public TMP_Text pointCase;
    public TMP_Text pointLettre;
    public TMP_Text pointMot;
    public TMP_Text pointTotal;
}
