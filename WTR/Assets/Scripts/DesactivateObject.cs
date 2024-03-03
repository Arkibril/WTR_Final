using UnityEngine;

public class DesactivateObject : MonoBehaviour
{
    public GameObject[] objectsToDeactivate;

    void Start()
    {
        // Parcourir tous les objets dans le tableau et les désactiver
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)  // Vérifier si l'objet existe avant de le désactiver
            {
                obj.SetActive(false);
            }

        }
    }
}
