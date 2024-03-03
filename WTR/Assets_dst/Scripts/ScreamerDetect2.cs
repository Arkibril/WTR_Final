using UnityEngine;

public class ScreamerDetect2 : MonoBehaviour
{

    public GameObject player;
    public GameObject Screamer;
    public GameObject World;


    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("PlayerHeadTag");
        Screamer = GameObject.FindGameObjectWithTag("Screamer");
        World = GameObject.FindGameObjectWithTag("Map");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(player);
            Destroy(World);
            Screamer.GetComponent<ActivationWithTime>().enabled = true;
            Destroy(this.gameObject);
        }
    }
}
