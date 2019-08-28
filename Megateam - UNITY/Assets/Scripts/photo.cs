using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class photo : MonoBehaviour
{
    public Camera mainCam;
    public List<string> photosCreated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            StartCoroutine(createPhoto());
        }
    }

    IEnumerator createPhoto()
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/photos");
        yield return new WaitForEndOfFrame();
        mainCam.targetTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 16);

        RenderTexture renderTexture = mainCam.targetTexture;
        Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
        renderResult.ReadPixels(rect, 0, 0);

        byte[] byteArray = renderResult.EncodeToJPG(50);

        int Index = 0;

        while(File.Exists(Application.persistentDataPath + "/photos/" + "photo_" + Index + ".jpg"))
        {
            Index++;
        }

        string finalPath = Application.persistentDataPath + "/photos/" + "photo_" + Index + ".jpg";

        photosCreated.Add(finalPath);
        File.WriteAllBytes(finalPath, byteArray);
        Debug.Log("Image Created At: " + finalPath);

        RenderTexture.ReleaseTemporary(renderTexture);
        mainCam.targetTexture = null;
    }
}
