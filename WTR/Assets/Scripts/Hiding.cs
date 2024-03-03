using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Hiding : MonoBehaviour, IDataPersistence<LanguageData>
{
    public Text hintText;
    public GameObject hiddenCam;
    public GameObject player;
    public LanguageData languageData;

    private bool isHiding = false;
    private bool canHide = false;

    public void LoadData(LanguageData data)
    {
        languageData = data;
    }

    public void SaveData(ref LanguageData data)
    {
     
    }

    private void Start()
    {
        hiddenCam.SetActive(false);
    }

    private void Update()
    {
        if (canHide && Input.GetKeyDown(KeyCode.E))
        {
            if (isHiding)
            {
                UnhidePlayer();
            }
            else
            {
                HidePlayer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHeadTag"))
        {
            canHide = true; // Le joueur peut se cacher car il est dans la zone
            hintText.gameObject.SetActive(true);

            if (isHiding)
            {
                hintText.text = (languageData.languageIndex == 1) ? "Appuyez sur E pour vous révéler" : "Press E to reveal yourself";
            }
            else
            {
                hintText.text = (languageData.languageIndex == 1) ? "Appuyez sur E pour vous cacher" : "Press E to hide";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHeadTag"))
        {
            canHide = false; // Le joueur ne peut plus se cacher car il a quitté la zone
            hintText.gameObject.SetActive(false);
        }
    }

    void HidePlayer()
    {
        if (canHide) // Le joueur ne peut se cacher que s'il est dans la zone
        {
            isHiding = true;
            player.SetActive(false);
            hiddenCam.SetActive(true);
            hintText.text = (languageData.languageIndex == 1) ? "Appuyez sur E pour vous révéler" : "Press E to reveal yourself";
        }
    }

    void UnhidePlayer()
    {
        if (canHide) // Le joueur ne peut se cacher que s'il est dans la zone
        {
            isHiding = false;
            player.SetActive(true);
            hiddenCam.SetActive(false);
            hintText.text = (languageData.languageIndex == 1) ? "Appuyez sur E pour vous cacher" : "Press E to hide";
        }
    }
}
