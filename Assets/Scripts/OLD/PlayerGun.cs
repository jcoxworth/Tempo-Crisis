using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public Transform cameraTransform;
    public LayerMask layerMask;
    public GameObject hitFX;
    public GameObject dummyBullet;
    public Transform dummyOrigin;
    public float shotDelay = 0.05f;
    public int BulletDamage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._access.isPaused || GameManager._access.isDead)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 origin = cameraTransform.position + (cameraTransform.forward * 0.5f);
            Vector3 direction = cameraTransform.TransformDirection(Vector3.forward);
            StartCoroutine(GunDelay(origin, direction));
        }
    }
    private IEnumerator GunDelay(Vector3 origin, Vector3 direction)
    {
        yield return new WaitForSeconds(shotDelay);
        ShootGun(origin, direction);
    }
    private void ShootGun(Vector3 origin, Vector3 direction)
    {
        Vector3 dummyAimPoint;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(origin, direction, out hit, Mathf.Infinity, layerMask))
        {
            dummyAimPoint = hit.point;
            Instantiate(hitFX, hit.point, Quaternion.LookRotation(hit.normal));
            hit.collider.transform.SendMessage("AddDamage", BulletDamage, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            dummyAimPoint = cameraTransform.position + (cameraTransform.forward * 100f);
        }

        Vector3 dummyDirection = dummyAimPoint - dummyOrigin.position;

        Instantiate(dummyBullet, dummyOrigin.position, Quaternion.LookRotation(dummyDirection));

    }
}
