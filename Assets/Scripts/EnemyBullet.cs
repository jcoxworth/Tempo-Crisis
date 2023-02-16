using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 10;
    public GameObject hitFX;
    private Rigidbody rb;
    public float speed = 100f;
    public LayerMask hitLayer;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._access.isPaused)
            return;
        rb.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime));

        BulletRay();
    }
    private void BulletRay()
    {
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f, hitLayer))
        {
            Instantiate(hitFX, hit.point, Quaternion.LookRotation(hit.normal));
            hit.transform.SendMessage("AddDamage", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }

    }
}
