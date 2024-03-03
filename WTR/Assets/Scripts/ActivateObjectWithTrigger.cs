using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectWithTrigger : MonoBehaviour
{
    public GameObject objectToActivated;
    public string TriggerName;
    void Start()
    {
        objectToActivated.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TriggerName)
        {
            objectToActivated.SetActive(true);
            Debug.Log("TheTriggerEnter");
        }
    }
}
