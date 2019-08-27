using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talker : MonoBehaviour
{
    public Dialogue[] talk1;
    GameObject focalReticle;

    void Start()
    {
        focalReticle = GameObject.Find("Focal Reticle");
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            CTRL.currentDialogue = talk1;
            focalReticle.transform.position = transform.parent.position + (Vector3.up * 3);
        }
    }

    void OnTriggerExit(Collider other)
    {
        CTRL.currentDialogue = null;

        focalReticle.transform.position = (Vector3.up * -3000);

    }
}
