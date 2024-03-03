using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LightManager2 : MonoBehaviour
{
    public bool Off;
    public AudioSource Switch;
    public Light[] ActiveLights;
    public Light markNightLight;
    private Light flashlight;
    private bool allLightsOff = false;  // Initialisez � false pour que markNightLight soit allum� au d�part
    private bool lightsTurnedOffOnce = false;

    // Ajout de variables pour la gestion du temps
    public float delayBeforeMarkLightOff = 5.0f;
    private bool markLightCanBeTurnedOff = false;

    private void Start()
    {
        flashlight = GameObject.FindGameObjectWithTag("FlashLight").GetComponent<Light>();
        markNightLight = GameObject.FindGameObjectWithTag("MarkNightLight").GetComponent<Light>();

        FindActiveLights();

        DisableAllLightsExceptFlashlight();

        if (this.gameObject.name == "untitled(Clone)" && Off == false && !lightsTurnedOffOnce)
        {
            ToggleLights();
            lightsTurnedOffOnce = true;
            StartCoroutine(DelayedMarkLightOff());
        }
    }

    private void Update()
    {
        // Vous pouvez activer cette partie si vous souhaitez r�activer les lumi�res avec la barre d'espace
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ToggleLights();
        //}
    }

    private IEnumerator DelayedMarkLightOff()
    {
        yield return new WaitForSeconds(delayBeforeMarkLightOff);
        markLightCanBeTurnedOff = true;
    }

    private void FindActiveLights()
    {
        // Trouve toutes les lumi�res actuellement allum�es dans la sc�ne
        ActiveLights = FindObjectsOfType<Light>().Where(light => light.enabled && light != markNightLight).ToArray();
    }

    private void ToggleLights()
    {
        // Active ou d�sactive toutes les lumi�res en fonction de l'�tat global
        foreach (Light light in ActiveLights)
        {
            light.enabled = allLightsOff;
        }

        markNightLight.enabled = !allLightsOff;
        allLightsOff = !allLightsOff;

        // V�rifie si toutes les lumi�res sont �teintes avant de d�sactiver markNightLight
        if (allLightsOff && markLightCanBeTurnedOff)
        {
            markNightLight.enabled = false;
            markLightCanBeTurnedOff = false; // Pour �viter de d�sactiver la lumi�re plusieurs fois
        }
        else if (!allLightsOff)
        {
            markLightCanBeTurnedOff = true; // R�initialise la possibilit� de d�sactiver markNightLight
        }
    }

    // Utilisez cette fonction pour activer ou d�sactiver les lumi�res avec un levier
    public void ToggleLightsWithLever()
    {
        FindActiveLights();
        ToggleLights();
    }

    private void DisableAllLightsExceptFlashlight()
    {
        foreach (Light light in ActiveLights)
        {
            if (light != flashlight)
            {
                light.enabled = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            ToggleLightsWithLever();
        }
    }
}
