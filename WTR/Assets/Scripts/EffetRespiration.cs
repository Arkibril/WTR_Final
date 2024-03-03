using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetRespiration : MonoBehaviour
{
    // Paramètres de l'effet de respiration
    public float frequenceRespiration = 0.5f; // Fréquence de la respiration
    public float amplitudeRespiration = 0.1f; // Amplitude de la respiration
    public float vitesseMouvement = 1.0f; // Vitesse de mouvement

    private Vector3 positionInitiale;

    void Start()
    {
        // Enregistre la position initiale de la caméra
        positionInitiale = transform.localPosition;

        // Démarre la coroutine pour simuler l'effet de respiration
        StartCoroutine(SimulerRespiration());
    }

    IEnumerator SimulerRespiration()
    {
        while (true)
        {
            // Calcule le décalage vertical en fonction de la respiration
            float offsetY = Mathf.Sin(Time.time * frequenceRespiration) * amplitudeRespiration;

            // Calcule le décalage horizontal en fonction de la respiration
            float offsetX = Mathf.Cos(Time.time * frequenceRespiration) * amplitudeRespiration;

            // Calcule le déplacement total en fonction de la respiration
            Vector3 deplacement = new Vector3(offsetX, offsetY, 0f);

            // Applique le déplacement à la position locale de la caméra avec une vitesse
            transform.localPosition = positionInitiale + deplacement * vitesseMouvement;

            yield return null;
        }
    }
}