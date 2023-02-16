using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    public Weapon playerWeapon;
    public WeaponAnimation playerWeaponAnim;
    public WeaponSound playerWeaponSound;
    public Mesh newWeaponMesh, newRecoilMesh, newBodyWeapon;

    public float newShotCoolDown = 0.5f;
    public int newMagazineCapacity = 15;
    public GameObject newBullet;
    public AudioClip newGunSound;
    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        CheckNullReference();

    }
    public void SetNewWeapon()
    {

        playerWeapon.bullet = newBullet;
        playerWeapon.shotCoolDown = newShotCoolDown;
        playerWeapon.magazineCapacity = newMagazineCapacity;
        playerWeaponAnim.weaponTransform.GetComponent<MeshFilter>().mesh = newWeaponMesh;
        playerWeaponAnim.recoilRenderer.transform.GetComponent<MeshFilter>().mesh = newRecoilMesh;
        playerWeapon.transform.GetComponent<WeaponSound>().shoot = newGunSound;
        playerWeapon.ChangeWeapon();
    }
    private void CheckNullReference()
    {
        if (!playerWeapon)
            playerWeapon = GameManager._access.player.transform.GetComponentInChildren<Weapon>();
        if (!playerWeaponAnim)
            playerWeaponAnim = GameManager._access.player.transform.GetComponentInChildren<WeaponAnimation>();

        if (!playerWeapon || !playerWeaponAnim)
            Debug.Log("WEapon chnager couldn't get player weapon");

    }
}
