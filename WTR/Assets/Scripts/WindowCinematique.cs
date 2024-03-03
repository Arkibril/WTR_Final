using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowCinematique : MonoBehaviour
{
    [Header("Intrusion Type")]
    public bool Spam;
    public bool Stay;

    private bool ItsGood;
    private bool TimeLeftFinish;
    public bool Tuto;
    bool isSpamming = false;

    [Header("Click / Time Number")]
    public int NumberSpam = 20;
    public int StayTime;

    private int NumberTouchEnter;
    public float LeftTime = 30;
    public float CurrentTime;
    public float Y;
    public float Ysave;
    float originalYPosition;

    [Header("GameObject")]
    public AudioSource music;
    public AudioSource windows;
    public GameObject failedObject; 
    public bool inverseOption;
    public GameObject tEST;
    public GameObject AnimationGameObject;

    private CameraShakeScript _cameraShakeScript;

    void Start()
    {
        originalYPosition = tEST.transform.position.y;
        Ysave = tEST.transform.position.y + 15;

        Tuto = SceneManager.GetActiveScene().name == "TUTO SCENE";
        Ysave = tEST.transform.position.y + 1;

        GameObject.FindGameObjectWithTag("MonsterScript").GetComponent<MonsterScript>().enabled = false;
        CurrentTime = LeftTime;
        _cameraShakeScript = GameObject.FindGameObjectWithTag("CameraHolder").GetComponent<CameraShakeScript>();
    }

    void Update()
    {
        Y = tEST.transform.position.y;
        CurrentTime -= Time.deltaTime;

        if ((LeftTime <= 0 && Y < Ysave && !Tuto) || (LeftTime <= 0 && Y < Ysave && Tuto))
        {
            EndGame();
        }

        if ((Y > Ysave && !Tuto) || (LeftTime < 0 && Y > Ysave && !Tuto))
        {
        }

        if ((Y > Ysave && Tuto) || (LeftTime < 0 && Y > Ysave && Tuto))
        {
            EndGame();
        }

        if (GameObject.FindGameObjectWithTag("MonsterCheck").GetComponent<MonsterCheck>().HesHere)
        {
            EndGame();
        }

        tEST.transform.Translate(Vector3.up * Time.deltaTime / 19);

        if (Input.GetKeyDown(KeyCode.E))
        {
            float newY = Y - Time.deltaTime / 1.5f;
            originalYPosition = Mathf.Max(newY, originalYPosition); 
            tEST.transform.position = new Vector3(tEST.transform.position.x, originalYPosition, tEST.transform.position.z);
            _cameraShakeScript.CameraShake();
        }

        if (CurrentTime <= 0)
        {
            PlayYourAnimation();
            this.enabled = false;
        }
    }

    void PlayYourAnimation()
    {
        AnimationGameObject.gameObject.GetComponent<Animator>().Play("2ndPart");
        Debug.Log("Animation de fin");
    }

    void OnTriggerStay(Collider other)
    {

    }

    void EndGame()
    {
        if (inverseOption)
        {
    
            failedObject.SetActive(true);
        }
        else
        {
  
            this.GetComponent<HesHere>().enabled = false;
            GameObject.FindGameObjectWithTag("MonsterScript").GetComponent<MonsterScript>().enabled = true;
        }

        WindowCinematique2(); 
    }

    void WindowCinematique2()
    {
        Debug.Log("Cinématique de fin");
    }
}
