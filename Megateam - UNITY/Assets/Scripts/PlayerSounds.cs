using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip[] GrassStepSounds;
    public AudioClip[] ConcreteStepSounds;
    public AudioClip[] GravelStepSounds;
    public Rigidbody PlayerRb;
    public stepSound soundType;

    private bool soundLoop;

    private void Update()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal")).normalized;
        if (move.x > 0 || move.y > 0)
        {
            if(!soundLoop)
            {
                StartCoroutine(stepSoundLoop());
            }
        }
    }

    IEnumerator stepSoundLoop()
    {
        soundLoop = true;
        yield return new WaitForSeconds(Input.GetButton("Fire3") ? 0.25f : 0.30f);
        switch(soundType)
        {
            case stepSound.Grass:
                SoundManager.instance.PlaySound(GrassStepSounds[Random.Range(0, GrassStepSounds.Length)], PlayerRb.transform.position, 0.01f, Random.Range(0.9f, 1.1f), true);
                break;
            case stepSound.Concrete:
                SoundManager.instance.PlaySound(ConcreteStepSounds[Random.Range(0, ConcreteStepSounds.Length)], PlayerRb.transform.position, 0.01f, Random.Range(0.9f, 1.1f), true);
                break;
            case stepSound.Gravel:
                SoundManager.instance.PlaySound(GravelStepSounds[Random.Range(0, GravelStepSounds.Length)], PlayerRb.transform.position, 0.01f, Random.Range(0.9f, 1.1f), true);
                break;
        }
        soundLoop = false;
    }

    public enum stepSound { Grass, Concrete, Gravel }
}
