using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float countdownTime = 300.0f; // 5 minutes en secondes (5 * 60)
    public Text countdownText;

    private float currentTime;

    void Start()
    {
        currentTime = countdownTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0.0f)
        {
            // Le temps est �coul�, vous pouvez ajouter ici le code � ex�cuter une fois que le timer est termin�.
            currentTime = 0.0f; // Pour �viter des valeurs n�gatives
        }

        // Convertissez les secondes en minutes pour afficher le temps restant
        float remainingMinutes = currentTime / 60;
        int minutes = Mathf.FloorToInt(remainingMinutes);
        int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
        countdownText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }
}
