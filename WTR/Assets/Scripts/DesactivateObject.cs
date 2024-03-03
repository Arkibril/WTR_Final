using UnityEngine;

public class DesactivateObject : MonoBehaviour
{
    public GameObject[] objectsToDeactivate;

    void Start()
    {
        // Parcourir tous les objets dans le tableau et les d�sactiver
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)  // V�rifier si l'objet existe avant de le d�sactiver
            {
                obj.SetActive(false);
            }

        }
    }
}
