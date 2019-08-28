using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talker : MonoBehaviour
{
    public DialogueTimeline dTimeline;
    GameObject focalReticle;

    void Start()
    {
        focalReticle = GameObject.Find("Focal Reticle");
    }

    void OnTriggerStay(Collider other)
    {
        if (dTimeline.willTalk && other.tag == "Player")
        {
            int which = -1;

            for (int i = 0; i < dTimeline.convo.Length; i++)
            {
                if (dTimeline.convo[i].match == dTimeline.progress)
                {
                    which = i;
                }
            }

            if (which >= 0)
            {
                CTRL.currentDialogue = dTimeline;
                focalReticle.transform.position = transform.parent.position + (Vector3.up * 3);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        CTRL.currentDialogue = null;

        focalReticle.transform.position = (Vector3.up * -3000);

    }
}
