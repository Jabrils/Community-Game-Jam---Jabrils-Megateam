using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public uint sourcesForPooling;

    public List<AudioSource> freeSources;
    public List<AudioSource> busySources;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for(int i = 0; i < sourcesForPooling; i++)
        {
            GameObject temp = new GameObject("Sound");
            temp.transform.SetParent(transform);
            freeSources.Add(temp.AddComponent<AudioSource>());
            temp.SetActive(false);
        }
    }

    public void PlaySound(AudioClip clip, Vector3 position, float volume = 1, float pitch = 1, bool is3d = false)
    {
        AudioSource temp = freeSources[0];
        temp.transform.position = position;
        temp.pitch = pitch;
        if(is3d)
        {
            temp.spatialBlend = 1;
            temp.minDistance = 5;
            temp.maxDistance = 7.5f;
        }
        StartCoroutine(AddToBusySources(temp, clip.length));
        temp.PlayOneShot(clip, volume);
    }

    public IEnumerator AddToBusySources(AudioSource audioSource, float time)
    {
        audioSource.gameObject.SetActive(true);
        freeSources.Remove(audioSource);
        busySources.Add(audioSource);
        yield return new WaitForSeconds(time);
        busySources.Remove(audioSource);
        freeSources.Add(audioSource);
        audioSource.gameObject.SetActive(false);
    }
}
