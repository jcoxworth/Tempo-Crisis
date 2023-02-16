using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    Weapon weapon;
    public AudioSource audioSource;

    public AudioClip shoot, reload, empty;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<Weapon>();

        weapon.onShoot += PlayShot;
        weapon.onReload += PlayReload;
        weapon.onDryFire += PlayEmpty;
    }

    void PlayShot()
    {
        audioSource.PlayOneShot(shoot);
    }
    void PlayReload()
    {
        audioSource.PlayOneShot(reload);

    }
    void PlayEmpty()
    {
        audioSource.PlayOneShot(empty);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
