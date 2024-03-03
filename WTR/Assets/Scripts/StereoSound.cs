using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StereoSound : MonoBehaviour
{
    public Transform playerTransform; 
    public Transform enemyTransform; 
    public float maxDistance = 10f;

    public AudioClip Sound; 

    [Range(0f, 1f)]
    public float maxVolume = 1f;

    public AudioSource audioSource;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("PlayerHeadTag").GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();

       
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1f; 
    }

    void Update()
    {
        // Calcul de la distance entre le joueur et le méchant
        float distance = Vector3.Distance(playerTransform.position, enemyTransform.position);

        // Calcul du volume en fonction de la distance
        float volume = Mathf.Clamp01(1 - (distance / maxDistance)) * maxVolume;

        // Ajustement du volume de l'AudioSource
        audioSource.volume = volume;

        // Déterminer la position du son en 3D
        Vector3 soundPosition = enemyTransform.position;

        // Ajuster la position en fonction de la distance (plus proche du joueur)
        soundPosition = Vector3.Lerp(soundPosition, playerTransform.position, 0.5f);

        // Définir la position 3D du son sur l'AudioSource
        audioSource.transform.position = soundPosition;

        // Lecture du son du battement de cœur
        if (!audioSource.isPlaying && volume > 0)
        {
            audioSource.PlayOneShot(Sound);
        }
    }
}
