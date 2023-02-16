using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAmmo : MonoBehaviour
{
    public Weapon playerWeapon;

    public RectTransform magazineRect;
    public GameObject pistolBulletUI;

    public List<GameObject> loadedBullets = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        playerWeapon.onShoot += UIShootBullet;
        playerWeapon.onReload += UIReloadMagazine;
        playerWeapon.onChangeWeapon += UIChangeMagazineType;
        UIReloadMagazine();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void UIChangeMagazineType()
    {

        for (int i = 0; i < loadedBullets.Count; i++)
        {
            GameObject b = loadedBullets[i];
            
            Destroy(b);
        }
        loadedBullets.Clear();
        print(loadedBullets.Count);
        /*
        for (int i = playerWeapon.currentMagazine; i < playerWeapon.magazineCapacity; i++)
        {
            GameObject p = Instantiate(pistolBulletUI, magazineRect);
            loadedBullets.Add(p);
        }
        print(loadedBullets.Count);*/
    }
    private void UIReloadMagazine()
    {
        if (playerWeapon.currentMagazine == playerWeapon.magazineCapacity)
            return;

        for (int i = playerWeapon.currentMagazine; i < playerWeapon.magazineCapacity; i++)
        {
            GameObject p = Instantiate(pistolBulletUI, magazineRect);
            loadedBullets.Add(p);
        }
    }
    private void UIShootBullet()
    {
        if (playerWeapon.currentMagazine < 1)
            return;
        GameObject b = loadedBullets[0];
        loadedBullets.Remove(loadedBullets[0]);
        Destroy(b);
    }
}
