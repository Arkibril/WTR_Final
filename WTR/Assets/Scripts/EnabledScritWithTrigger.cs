using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledScritWithTrigger : MonoBehaviour
{
    public MonoBehaviour EnabledScript;
    public string TriggerName;


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == TriggerName)
        {
            Debug.Log("OnTriggerStay is called");
            EnabledScript.enabled = true;
        }
    }

}
