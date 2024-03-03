using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Nom du script que vous voulez d�sactiver
    public string scriptAInactiver;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Obtenez le type du script � partir de son nom
            System.Type scriptType = System.Type.GetType(scriptAInactiver);

            // V�rifiez si le scriptType est non nul
            if (scriptType != null)
            {
                // Obtenez le composant du script sur l'objet en collision
                MonoBehaviour scriptComponent = other.gameObject.GetComponent(scriptType) as MonoBehaviour;

                // Si le composant est trouv�, d�sactivez-le
                if (scriptComponent != null)
                {
                    scriptComponent.enabled = false;
                    Debug.Log("Le script a �t� d�sactiv� !");
                }
                else
                {
                    Debug.LogError("Le script n'a pas �t� trouv� sur l'objet en collision !");
                }
            }
            else
            {
                Debug.LogError("Le type de script n'a pas �t� trouv� !");
            }
        }
    }
}