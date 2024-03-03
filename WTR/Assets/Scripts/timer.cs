using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Text timerText;
    private float totalTime = 300.0f; // 5 minutes en secondes
    private float currentTime;

    private void Start()
    {
        currentTime = totalTime;
    }

    private void Update()
    {
        // Mettre � jour le temps restant
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            // G�rer ce qui se passe lorsque le temps est �coul�
        }

        // Convertir le temps en minutes et secondes
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        // Mettre � jour le texte du minuteur
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}