using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Sprite characterSpr;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<Renderer>();

        rend.material.SetTexture("_BaseMap", characterSpr.texture);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
