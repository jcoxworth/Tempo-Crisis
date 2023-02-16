using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    Weapon weapon;
    public Transform weaponTransform;
    private Vector3 weaponOriginalRotation;
    private Vector3 weaponOriginalPos;

    public float weaponRecoilRotation;
    public Vector3 weaponRotationAxis = Vector3.right;
    public Vector3 weaponMovementAxis = Vector3.up + Vector3.back;

    public Transform recoilObjectTransform;
    public float recoilObjectMovement;
    public Vector3 recoilObjectMovementDirection = Vector3.up;

    public MeshRenderer playerBodyWeapon;
    [HideInInspector]public MeshRenderer weaponRenderer, recoilRenderer;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<Weapon>();
        weapon.onShoot += Recoil;
        weaponOriginalRotation = weaponTransform.localEulerAngles;
        weaponOriginalPos = weaponTransform.localPosition;

        weaponRenderer = weaponTransform.GetComponent<MeshRenderer>();
        recoilRenderer = recoilObjectTransform.GetComponent<MeshRenderer>();

    }

    void Recoil()
    {
        recoilObjectTransform.localPosition = recoilObjectMovementDirection * recoilObjectMovement;
        weaponTransform.localRotation = Quaternion.Euler( weaponOriginalRotation + (weaponRotationAxis * weaponRecoilRotation));
        weaponTransform.localPosition = weaponOriginalPos + weaponMovementAxis;
        //Debug.Break();
    }
    void ResetPositions()
    {
        recoilObjectTransform.localPosition = Vector3.zero;
        weaponTransform.localPosition = weaponOriginalPos;
        weaponTransform.localRotation = Quaternion.Euler(weaponOriginalRotation);
    }
    // Update is called once per frame
    void Update()
    {
        if(playerBodyWeapon)
            playerBodyWeapon.enabled = !weapon.isActive;

        if (!weapon.isActive)
        {
            recoilRenderer.enabled = false;
            weaponRenderer.enabled = false;
            ResetPositions();
            return;
        }
        recoilRenderer.enabled = true;
        weaponRenderer.enabled = true;


        if (weapon.currentMagazine > 0)
        {
            recoilObjectTransform.localPosition = Vector3.MoveTowards(recoilObjectTransform.localPosition, Vector3.zero, Time.deltaTime * 2.5f);
        }

        weaponTransform.localPosition = Vector3.Lerp(weaponTransform.localPosition, weaponOriginalPos, Time.deltaTime * 2.5f);

        weaponTransform.localRotation = Quaternion.Lerp(weaponTransform.localRotation, Quaternion.Euler(weaponOriginalRotation), Time.deltaTime * 10f);
    }
}
