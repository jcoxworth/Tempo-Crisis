using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSound : MonoBehaviour
{
    ExtraObject extra;

    private AudioSource source;
    public AudioClip[] damageClips;
    public AudioClip[] destroyClips;
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 1f;
        source.minDistance = 1f;
        source.maxDistance = 8f;
        source.volume = 0.7f;

        extra = GetComponent<ExtraObject>();

        extra.onDamage += DamageSound;
        extra.onDestroy += DestroySound;
    }
    private void DestroySound()
    {
        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(destroyClips[Random.Range(0, destroyClips.Length)]);
    }
    private void DamageSound()
    {
        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(damageClips[Random.Range(0, damageClips.Length)]);
    }

   
}
