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
    private bool allLightsOff = false;  // Initialisez à false pour que markNightLight soit allumé au départ
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
        // Vous pouvez activer cette partie si vous souhaitez réactiver les lumières avec la barre d'espace
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
        // Trouve toutes les lumières actuellement allumées dans la scène
        ActiveLights = FindObjectsOfType<Light>().Where(light => light.enabled && light != markNightLight).ToArray();
    }

    private void ToggleLights()
    {
        // Active ou désactive toutes les lumières en fonction de l'état global
        foreach (Light light in ActiveLights)
        {
            light.enabled = allLightsOff;
        }

        markNightLight.enabled = !allLightsOff;
        allLightsOff = !allLightsOff;

        // Vérifie si toutes les lumières sont éteintes avant de désactiver markNightLight
        if (allLightsOff && markLightCanBeTurnedOff)
        {
            markNightLight.enabled = false;
            markLightCanBeTurnedOff = false; // Pour éviter de désactiver la lumière plusieurs fois
        }
        else if (!allLightsOff)
        {
            markLightCanBeTurnedOff = true; // Réinitialise la possibilité de désactiver markNightLight
        }
    }

    // Utilisez cette fonction pour activer ou désactiver les lumières avec un levier
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
