using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void OnEnable()
    {
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
