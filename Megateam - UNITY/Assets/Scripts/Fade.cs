//By Oliver Geneser A.k.a. Oliv6176

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fade : MonoBehaviour
{
    [Header("Image to fade")]
    public Image image;

    [Header("Text to fade")]
    public TMP_Text text;

    [Header("Hold on screen for sec")]
    public float holdForSec = 0f;

    [Header("Fade in time")]
    public float fadeIn;

    [Header("Fade out time")]
    public float fadeOut;

    [Header("Returns true when fade is completed")]
    public bool isCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        if (image != null)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            StartCoroutine(fadeImage());
        }
        if (text != null)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
            StartCoroutine(fadeText());
        }
        if(image == null && text == null)
        {
            Debug.Log("No Image or text");
        }
    }

    private IEnumerator fadeImage()
    {
        Color tempColor = image.color;
        tempColor.a = 1f;

        for (float step = 0.01f; step < fadeIn; step += Time.deltaTime)
        {
            image.color = Color.Lerp(new Color(tempColor.r, tempColor.g, tempColor.b, 0f), tempColor, Mathf.Min(1, step / fadeIn));
            yield return null;
        }

        yield return new WaitForSeconds(holdForSec);

        for (float step = 0.01f; step < fadeOut; step += Time.deltaTime)
        {
            image.color = Color.Lerp(tempColor, new Color(tempColor.r, tempColor.g, tempColor.b, 0f), Mathf.Min(1, step / fadeOut));
            yield return null;
        }

        yield return isCompleted = true;

    }
    private IEnumerator fadeText()
    {
        Color tempColor = text.color;
        tempColor.a = 1f;

        for (float step = 0.01f; step < fadeIn; step += Time.deltaTime)
        {
            text.color = Color.Lerp(new Color(tempColor.r, tempColor.g, tempColor.b, 0f), tempColor, Mathf.Min(1, step / fadeIn));
            yield return null;
        }

        yield return new WaitForSeconds(holdForSec);

        for (float step = 0.01f; step < fadeOut; step += Time.deltaTime)
        {
            text.color = Color.Lerp(tempColor, new Color(tempColor.r, tempColor.g, tempColor.b, 0f), Mathf.Min(1, step / fadeOut));
            yield return null;
        }

        yield return isCompleted = true;

    }

}
