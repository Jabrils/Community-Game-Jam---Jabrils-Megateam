using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCTRL : MonoBehaviour
{
    GameObject target;
    float camHeight = 0;
    // Cam Config Index
    int ccInd;
    float timer;
    float[] camRadians = new float[]
    {
        0,
        3 * Mathf.PI / 2,
        Mathf.PI,
        Mathf.PI / 2,

    };

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
            transform.position = new Vector3(target.transform.position.x + Mathf.Sin(camRadians[ccInd]) * (3 + camHeight), 3 + ((camHeight / 7) * 2), target.transform.position.z + Mathf.Cos(camRadians[ccInd]) * (3 + camHeight));
    }

    // Update is called once per frame
    void Update()
    {
        camHeight -= Input.GetAxis("Mouse ScrollWheel") * 5;

        camHeight = Mathf.Clamp(camHeight, 0, 7);

        if (Input.GetButtonDown("Fire1"))
        {
            timer = 0;

            ccInd++;

            if (ccInd >= camRadians.Length)
            {
                ccInd = 0;
            }
        }

        timer += Time.deltaTime;

        // LOL Sorry for this super ugly wonky camera controller equation, I'm not slowing down to comment :'(
        if (timer <= .25f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x + Mathf.Sin(camRadians[ccInd]) * (3 + camHeight), 3 + ((camHeight / 7) * 2), target.transform.position.z + Mathf.Cos(camRadians[ccInd]) * (3 + camHeight)), timer / 1);
        }
        else
        {
            transform.position = new Vector3(target.transform.position.x + Mathf.Sin(camRadians[ccInd]) * (3 + camHeight), 3 + ((camHeight / 7) * 2), target.transform.position.z + Mathf.Cos(camRadians[ccInd]) * (3 + camHeight));
        }

        // Look at player
        transform.LookAt(target.transform.position + (Vector3.up*2));
    }
}
