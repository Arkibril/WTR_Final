using UnityEngine;

public class HeartbeatScript : MonoBehaviour
{
    public Transform playerTransform; // Référence au transform du joueur
    public Transform enemyTransform; // Référence au transform du méchant

    public AudioClip heartbeatSound; // Son du battement de cœur
    public AudioSource audioSource; // Composant AudioSource pour jouer le son

    public float maxDistance = 10f; // Distance maximale à laquelle le son est entendu
    public float maxVolume = 1f; // Volume maximal du battement de cœur

    public float minRate = 1f; // Taux de battement minimal
    public float maxRate = 5f; // Taux de battement maximal
    private float currentRate = 1f; // Taux de battement actuel

    private float timeSinceLastHeartbeat = 0f; // Temps écoulé depuis le dernier battement


    public void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("PlayerHeadTag").GetComponent<Transform>();
    }
    void Update()
    {
        // Calcul de la distance entre le joueur et le méchant
        float distance = Vector3.Distance(playerTransform.position, enemyTransform.position);

        // Calcul du volume en fonction de la distance
        float volume = Mathf.Clamp01(1 - (distance / maxDistance)) * maxVolume;

        // Ajustement du volume de l'AudioSource
        audioSource.volume = volume;

        // Ajuster la fréquence des battements en fonction de la distance
        currentRate = Mathf.Lerp(minRate, maxRate, 1 - (distance / maxDistance));

        // Déterminer la position du son en 3D
        Vector3 soundPosition = enemyTransform.position;

        // Ajuster la position en fonction de la distance (plus proche du joueur)
        soundPosition = Vector3.Lerp(soundPosition, playerTransform.position, 0.5f);

        // Définir la position 3D du son sur l'AudioSource
        audioSource.transform.position = soundPosition;

        // Vérifier le temps écoulé depuis le dernier battement
        timeSinceLastHeartbeat += Time.deltaTime;

        // Vérifier si le temps entre les battements est écoulé
        if (timeSinceLastHeartbeat >= 1 / currentRate)
        {
            // Réinitialiser le temps
            timeSinceLastHeartbeat = 0f;

            // Lecture du son du battement de cœur
            if (volume > 0)
            {
                audioSource.PlayOneShot(heartbeatSound);
            }
        }
    }
}
