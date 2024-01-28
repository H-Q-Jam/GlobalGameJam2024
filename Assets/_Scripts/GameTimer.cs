using System;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] float matchDelay;
    [SerializeField] float timeRunning;
    [SerializeField] TMP_Text timeText;

    private bool isGameRunning;
    public static event Action OnTimerOver;

    private void OnEnable()
    {
        isGameRunning = true;
        timeRunning = matchDelay;
    }

    private void FixedUpdate()
    {
        if (isGameRunning)
        {
            if (timeRunning > 0)
            {
                timeRunning -= Time.fixedDeltaTime;
                timeRunning = Mathf.Max(timeRunning, 0);
                int seconde = Mathf.FloorToInt(timeRunning % 60);
                int minute = Mathf.FloorToInt((timeRunning - seconde) / 60);
                
                timeText.text = (minute < 10 ? "0" : "") + minute + " : " + (seconde < 10 ? "0" : "") + seconde;
            }
            if (timeRunning <= 0)
            {
                Debug.Log("End");
                OnTimerOver?.Invoke();
            }
        }
    }
}
