using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetRespiration : MonoBehaviour
{
    // Param�tres de l'effet de respiration
    public float frequenceRespiration = 0.5f; // Fr�quence de la respiration
    public float amplitudeRespiration = 0.1f; // Amplitude de la respiration
    public float vitesseMouvement = 1.0f; // Vitesse de mouvement

    private Vector3 positionInitiale;

    void Start()
    {
        // Enregistre la position initiale de la cam�ra
        positionInitiale = transform.localPosition;

        // D�marre la coroutine pour simuler l'effet de respiration
        StartCoroutine(SimulerRespiration());
    }

    IEnumerator SimulerRespiration()
    {
        while (true)
        {
            // Calcule le d�calage vertical en fonction de la respiration
            float offsetY = Mathf.Sin(Time.time * frequenceRespiration) * amplitudeRespiration;

            // Calcule le d�calage horizontal en fonction de la respiration
            float offsetX = Mathf.Cos(Time.time * frequenceRespiration) * amplitudeRespiration;

            // Calcule le d�placement total en fonction de la respiration
            Vector3 deplacement = new Vector3(offsetX, offsetY, 0f);

            // Applique le d�placement � la position locale de la cam�ra avec une vitesse
            transform.localPosition = positionInitiale + deplacement * vitesseMouvement;

            yield return null;
        }
    }
}