using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public TMPro.TextMeshProUGUI[] taskText;

    public Flechette _flechette;
    public Fireplace _fireplace;
    public BreadPlate _breadPlate;
    public PileOfClothes _pileOfClothes;
    public RecordPlayer _recordPlayer;
    public bool onIt;
    public GameObject taskCamera;
    public GameObject player;

    private void Start()
    {
        taskCamera.SetActive(false);
        player = GameObject.FindWithTag("PlayerHeadTag");
    }

    private void Update()
    {
        if (_flechette.done) taskText[0].text = "<s> Faire une partie de jeu de fléchettes</s>";
        else taskText[0].text = "Faire une partie de jeu de fléchettes";

        if (_fireplace.done) taskText[1].text = "<s> Allimenter le feu de cheminée</s>";
        else taskText[1].text = "Allimenter le feu de cheminée";

        if (_breadPlate.done) taskText[2].text = "<s> Faire des toasts</s>";
        else taskText[2].text = "Faire des toasts";

        if (_pileOfClothes.done) taskText[3].text = "<s> Faire une lessive</s>";
        else taskText[3].text = "Faire une lessive";

        if (_recordPlayer.done) taskText[4].text = "<s> Mettre de la musique</s>";
        else taskText[4].text = "Mettre de la musique";

        if (onIt)
        {
            player.SetActive(false);
            taskCamera.SetActive(true);
            this.GetComponent<AudioSource>().Play();

            if (Input.GetKeyDown(KeyCode.E))
            {
                onIt = false;
                player.SetActive(true);
                taskCamera.SetActive(false);
                this.GetComponent<AudioSource>().Play();
            }
        }
    }


}
