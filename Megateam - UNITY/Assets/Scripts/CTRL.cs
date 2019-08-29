using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTRL : MonoBehaviour
{
    public Vector4 feet;
    public float WalkSpeed = 150;
    public float RunSpeed = 225;
    public float JumpForce = 5;
    public GameObject player;
    public LayerMask GroundLayer;
    GameObject chatbox;
    Rigidbody playerRB;
    Vector3 camOffset;
    public static DialogueTimeline currentDialogue;

    Camera mainCam;
    List<GameObject> billboard = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Get ref to main camera, this method is faster
        mainCam = Camera.main;

        // Get ref to our player
        //player = GameObject.FindGameObjectWithTag("Player");

        // 
        chatbox = GameObject.Find("Chatting");
        chatbox.SetActive(false);

        // 
        DialogueSystem.Init(chatbox);

        // Get ref to our player's rigidbody
        playerRB = player.GetComponent<Rigidbody>();

        camOffset = mainCam.transform.position / 2;

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

        //mainCam.transform.position = player.transform.localPosition + camOffset;
    }

    void PlayerControls()
    {
        // use speed to scale up or down our speed
        float spd = Input.GetButton("Fire3") ? RunSpeed : WalkSpeed;

        // get a ref to movement, using the player's directions right & forward, but the inverse of that since the player is facing the inverse direction of the camera by looking at it, then scale that by the horizontal & vertical inputs, as well as speed & deltatime
        Vector3 move = (player.transform.forward * -Input.GetAxisRaw("Vertical") + player.transform.right * -Input.GetAxisRaw("Horizontal")).normalized;

        playerRB.velocity = new Vector3(playerRB.velocity.x * 0.5f, playerRB.velocity.y, playerRB.velocity.z * 0.5f);

        // If we aren't chatting
        if (DialogueSystem.state == DialogueSystem.State.Finished)
        {
            // Move our player's rigidbody
            playerRB.AddForce(move * spd * Time.deltaTime, ForceMode.VelocityChange);

            // Press jump to make our player jump
            if (Input.GetButtonDown("Jump"))
            {
                /*if(Physics.Raycast(player.transform.position + new Vector3(0,feet,0), Vector3.down, out RaycastHit hit, 0.25f, GroundLayer))
                {
                    playerRB.AddForce(Vector3.up * JumpForce , ForceMode.Impulse);
                }*/
                Collider[] hits = Physics.OverlapBox(player.transform.position + new Vector3(0, feet.x, 0), new Vector3(feet.y, feet.z, feet.w), player.transform.rotation, GroundLayer);
                if (hits.Length > 0)
                {
                    playerRB.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                }
            }
        }
        // 
        if (currentDialogue != null)
        {
            if (Input.GetKeyDown(KeyCode.Z) && currentDialogue.willTalk)
            {
                int which = -1;

                for (int i = 0; i < currentDialogue.convo.Length; i++)
                {
                    if (currentDialogue.convo[i].match == currentDialogue.progress)
                    {
                        which = i;
                    }
                }

                if (which >= 0)
                {
                    DialogueSystem.Chat(currentDialogue.convo[which].lines, currentDialogue.convo[which].action);
                }
            }
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
            /*// we need a reference to the main camera's position so we can modify it, we don't want the objects to rotate on the y axis
            Vector3 dontChangeY = mainCam.transform.position;

            // here we use the main camera's position, but force the y to be 0, so y rotation on our billboards never changes
            dontChangeY.y = 0;

            // now make them look at the new modified main camera's position
            g.transform.LookAt(dontChangeY);*/

            //Gets direction towards camera
            Vector3 camDir = (mainCam.transform.position - g.transform.position).normalized;
            //Rotates towards camera
            g.transform.eulerAngles = new Vector3(0, Quaternion.LookRotation(camDir, Vector3.up).eulerAngles.y, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (player)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(player.transform.position + new Vector3(0, feet.x, 0), new Vector3(feet.y, feet.z, feet.w));
        }
    }
}
