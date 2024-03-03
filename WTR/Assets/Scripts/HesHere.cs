using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class HesHere : MonoBehaviour
{
    [Header("Intrusion Type")]
    public bool Spam;
    public bool Stay;

    private bool ItsGood;
    private bool TimeLeftFinish;
    public bool Tuto = false;

    [Header("Click / Time Number")]
    public int NumberSpam = 20;
    public int StayTime;

    private int NumberTouchEnter;
    public float LeftTime = 30;
    public float CurrentTime;
    public float Y;
    public float Ysave;

    [Header("Audio")]
    public AudioSource music;
    public AudioSource windows;
    public AudioSource audioSourceWhistle;
    public AudioClip Whistle;

    [Header("GameObject")]
    public GameObject Monster;
    public Transform MonsterSpawn;
    public Vector3 spawnOffset;

    public GameObject Indicator;
    public GameObject tEST;
    private CameraShakeScript _cameraShakeScript;

    float xSize = 8.0f;
    float ySize = 8.0f;
    float zSize = 8.0f;

    public Animator successAnimator;

    void Start()
    {
        Ysave = tEST.transform.position.y + 15;

        if (SceneManager.GetActiveScene().name == "Tuto")
        {
            Tuto = true;
        }
        else
        {
            Tuto = false;
        }
        audioSourceWhistle.PlayOneShot(Whistle);

        Ysave = tEST.transform.position.y + 1;
        GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
        MonsterScript.GetComponent<MonsterScript>().enabled = false;
        CurrentTime = LeftTime;
        _cameraShakeScript = GameObject.FindGameObjectWithTag("CameraHolder").GetComponent<CameraShakeScript>();
        audioSourceWhistle.PlayOneShot(Whistle);

    }

    void Update()
    {
        Y = tEST.transform.position.y;

        CurrentTime -= 1 * Time.deltaTime;

        if (LeftTime > 0 && Ysave > Y)
        {
            Indicator.SetActive(true);
            LeftTime -= 1 * Time.deltaTime;
            tEST.transform.Translate(Vector3.up * Time.deltaTime / 37.5f);
            windows.Play();
        }

        if ((LeftTime <= 0 && Y < Ysave && Tuto == false) || (LeftTime <= 0 && Y < Ysave && Tuto == true))
        {
            Indicator.SetActive(false);
            windows.Stop();
            LeftTime = 15f;
            PlaySuccessAnimation();
            DisableComponents();
        }

        if ((Y > Ysave && Tuto == false) || (LeftTime < 0 && Y > Ysave && Tuto == false))
        {
            Indicator.SetActive(false);
            windows.Stop();
            //PlayFailureAnimation();
            SpawnMonster();
            DisableComponents();
        }

        if ((Y > Ysave && Tuto == true) || (LeftTime < 0 && Y > Ysave && Tuto == true))
        {
            Indicator.SetActive(false);
            PlayFailureAnimation();
            DisableComponents();
        }

        if (GameObject.FindGameObjectWithTag("MonsterCheck").GetComponent<MonsterCheck>().HesHere == true)
        {
            DisableComponents();
        }

        tEST.transform.Translate(Vector3.up * Time.deltaTime / 19);
    }

    void OnTriggerStay(Collider other)
    {
        if (tEST.transform.position.y + 1 >= Ysave)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                tEST.transform.Translate(Vector3.down * Time.deltaTime / 1.5f);
                _cameraShakeScript.CameraShake();
            }
        }
    }

    void PlaySuccessAnimation()
    {
        Debug.Log("Playing Success Animation");
        //successAnimator.StopPlayback();
        successAnimator.Play("SuccessAnimation");
        //successAnimator.StopPlayback();
    }



    void PlayFailureAnimation()
    {
        
    }

    void SpawnMonster()
    {
        Vector3 spawnPosition = MonsterSpawn.position + spawnOffset;
        Quaternion spawnRotation = MonsterSpawn.rotation;

        GameObject spawnedPrefab = Instantiate(Monster, spawnPosition, spawnRotation);
    }

    void DisableComponents()
    {
        GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
        MonsterScript.GetComponent<MonsterScript>().enabled = false;
        this.GetComponent<HesHere>().enabled = false;
    }

    string GetCurrentSceneName()
    {
        // Récupérer le nom de la scène actuelle
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name;
    }
}
