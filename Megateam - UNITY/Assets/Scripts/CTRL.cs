﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTRL : MonoBehaviour
{
    GameObject player;
    Rigidbody playerRB;
    Vector3 camOffset;

    Camera mainCam;
    List<GameObject> billboard = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Get ref to main camera, this method is faster
        mainCam = Camera.main;

        // Get ref to our player
        player = GameObject.FindGameObjectWithTag("Player");

        // Get ref to our player's rigidbody
        playerRB = player.GetComponent<Rigidbody>();

        camOffset = mainCam.transform.position/2;

        GetAllBillBoardObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        FaceBillboardsToCamera();

        PlayerControls();

        mainCam.transform.position = player.transform.position + camOffset;
    }

    void PlayerControls()
    {
        // use speed to scale up or down our speed
        float spd = Input.GetButton("Fire3") ? 7f : 3f;

        // get a ref to movement, using the player's directions right & forward, but the inverse of that since the player is facing the inverse direction of the camera by looking at it, then scale that by the horizontal & vertical inputs, as well as speed & deltatime
        Vector3 move = (-player.transform.right * Input.GetAxis("Horizontal") - player.transform.forward * Input.GetAxis("Vertical")) * Time.deltaTime * spd; ;

        // Move our player's rigidbody
        playerRB.MovePosition(playerRB.position + move);

        // Press jump to make our player jump
        if (Input.GetButtonDown("Jump"))
        {
            playerRB.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }

    void GetAllBillBoardObjects()
    {
        // Loop through all objects in the scene
        foreach (GameObject t in FindObjectsOfType<GameObject>())
        {
            // If they are on the Billboard layer,
            if (t.layer == 9)
            {
                // Add them to our billboard list
                billboard.Add(t);
            }
        }
    }

    void FaceBillboardsToCamera()
    {
        // for each object in billboard,
        foreach (GameObject g in billboard)
        {
            // we need a reference to the main camera's position so we can modify it, we don't want the objects to rotate on the y axis
            Vector3 dontChangeY = mainCam.transform.position;

            // here we use the main camera's position, but force the y to be 0, so y rotation on our billboards never changes
            dontChangeY.y = 0;

            // now make them look at the new modified main camera's position
            g.transform.LookAt(dontChangeY);
        }
    }
}
