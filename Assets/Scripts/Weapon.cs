using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isActive = true;
    public bool isShooting = false;
    public bool hasInfiniteMagazine = false;
    private float timeStamp;
    public float shotCoolDown = 0.5f;

    public int magazineCapacity = 10;
    public int currentMagazine = 10;

    public GameObject bullet;
    public GameObject muzzleFlash;
    public Transform gunBarrel;
    public delegate void OnChangeWeapon();
    public OnChangeWeapon onChangeWeapon;
    public delegate void OnShoot();
    public OnShoot onShoot;

    public delegate void OnDryFire();
    public OnDryFire onDryFire;

    public delegate void OnReload();
    public OnReload onReload;

    private float randomCoolDown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentMagazine = magazineCapacity;
        GameManager._access.onFinishLevel += Reload;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;

        if (isShooting)
            Shooting();
    }
    public void ChangeWeapon()
    {
        onChangeWeapon?.Invoke();
        currentMagazine = 0;
        StartCoroutine(DelayedReload());
    }
    private IEnumerator DelayedReload()
    {
        yield return new WaitForSeconds(0.25f);
        Reload();
    }
    public void Reload()
    {
        if (currentMagazine == magazineCapacity)
            return;
        onReload?.Invoke();
       // Debug.Log("trying to reload");
        currentMagazine = magazineCapacity;
    }

    private void Shooting()
    {
        timeStamp += Time.deltaTime;

        if (timeStamp > shotCoolDown + randomCoolDown)
        {
            ShootOne();
            isShooting = false;
        }
    }
    
    public void ShootOne()
    {
        if (currentMagazine < 1)
        {
            onDryFire?.Invoke();
            isShooting = false;
            timeStamp = 0;
            return;
        }


        //AimGunRig();
        //EnemyAttack will add a counting function to this delegate so that the enemy can count shots
        onShoot?.Invoke();
        GameObject b = Instantiate(bullet, gunBarrel.position, gunBarrel.rotation);
        GameObject mf = Instantiate(muzzleFlash, gunBarrel.position, gunBarrel.rotation);

        timeStamp = 0;
        
        if (currentMagazine > 0)
            currentMagazine -= 1;

        if (hasInfiniteMagazine)
        {
            currentMagazine = magazineCapacity;
            randomCoolDown = Random.Range(shotCoolDown * -0.55f, shotCoolDown * 0.25f);
        }
        else
        {
            randomCoolDown = 0f;
        }
    }


    public void FastCoolDown()
    {
        timeStamp += shotCoolDown;
    }
}
