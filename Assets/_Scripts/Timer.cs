using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float matchDelay;
    [SerializeField] float timeRunning;
    [SerializeField] TMP_Text timeText;
    private void FixedUpdate()
    {
        if (timeRunning < matchDelay)
        {
            timeRunning += Time.fixedDeltaTime;
            int seconde = Mathf.FloorToInt(timeRunning % 60);
            int minute = Mathf.FloorToInt((timeRunning - seconde) / 60);
            
            timeText.text = (minute < 10 ? "0" : "") + minute + " : " + (seconde < 10 ? "0" : "") + seconde;
        }
        if (timeRunning >= matchDelay)
        {
            Debug.Log("End");
        }
    }
}
