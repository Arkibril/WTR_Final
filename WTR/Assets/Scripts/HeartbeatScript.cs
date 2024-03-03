using UnityEngine;

public class HeartbeatScript : MonoBehaviour
{
    public Transform playerTransform; // R�f�rence au transform du joueur
    public Transform enemyTransform; // R�f�rence au transform du m�chant

    public AudioClip heartbeatSound; // Son du battement de c�ur
    public AudioSource audioSource; // Composant AudioSource pour jouer le son

    public float maxDistance = 10f; // Distance maximale � laquelle le son est entendu
    public float maxVolume = 1f; // Volume maximal du battement de c�ur

    public float minRate = 1f; // Taux de battement minimal
    public float maxRate = 5f; // Taux de battement maximal
    private float currentRate = 1f; // Taux de battement actuel

    private float timeSinceLastHeartbeat = 0f; // Temps �coul� depuis le dernier battement


    public void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("PlayerHeadTag").GetComponent<Transform>();
    }
    void Update()
    {
        // Calcul de la distance entre le joueur et le m�chant
        float distance = Vector3.Distance(playerTransform.position, enemyTransform.position);

        // Calcul du volume en fonction de la distance
        float volume = Mathf.Clamp01(1 - (distance / maxDistance)) * maxVolume;

        // Ajustement du volume de l'AudioSource
        audioSource.volume = volume;

        // Ajuster la fr�quence des battements en fonction de la distance
        currentRate = Mathf.Lerp(minRate, maxRate, 1 - (distance / maxDistance));

        // D�terminer la position du son en 3D
        Vector3 soundPosition = enemyTransform.position;

        // Ajuster la position en fonction de la distance (plus proche du joueur)
        soundPosition = Vector3.Lerp(soundPosition, playerTransform.position, 0.5f);

        // D�finir la position 3D du son sur l'AudioSource
        audioSource.transform.position = soundPosition;

        // V�rifier le temps �coul� depuis le dernier battement
        timeSinceLastHeartbeat += Time.deltaTime;

        // V�rifier si le temps entre les battements est �coul�
        if (timeSinceLastHeartbeat >= 1 / currentRate)
        {
            // R�initialiser le temps
            timeSinceLastHeartbeat = 0f;

            // Lecture du son du battement de c�ur
            if (volume > 0)
            {
                audioSource.PlayOneShot(heartbeatSound);
            }
        }
    }
}
