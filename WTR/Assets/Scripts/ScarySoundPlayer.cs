using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarySoundPlayer : MonoBehaviour
{
    public AudioClip[] scarySounds;  // Ajoutez vos fichiers audio effrayants dans l'inspecteur Unity

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Lancez la coroutine pour jouer des sons de manière aléatoire
        StartCoroutine(PlayScarySounds());
    }

    private IEnumerator PlayScarySounds()
    {
        while (true)
        {
            // Attendez un temps aléatoire avant de jouer le son suivant
            yield return new WaitForSeconds(Random.Range(0.5f, 150f));

            // Choisissez un son effrayant au hasard dans le tableau
            if (scarySounds.Length > 0)
            {
                AudioClip randomScarySound = scarySounds[Random.Range(0, scarySounds.Length)];

                // Jouez le son
                audioSource.clip = randomScarySound;
                audioSource.Play();
            }
        }
    }
}
