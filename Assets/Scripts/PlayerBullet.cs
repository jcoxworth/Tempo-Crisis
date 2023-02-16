using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 10;
    public GameObject hitFX;
    private Rigidbody rb;
    public float speed = 100f;
    public float lifeTime = 1.5f;
    public LayerMask enemyLayer;
    public LayerMask hitLayer;

    public bool isArmorPiercing = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(BulletLife());
    }
    private IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(lifeTime);
        GameScore._access.EndHitStreak();
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager._access.isPaused)
            return;

        rb.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime));

        if (isArmorPiercing)
            ArmorPiercingRay();
        else
            BulletRay();
    }
    private void ArmorPiercingRay()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 25f, enemyLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].transform.SendMessage("AddDamage", damage, SendMessageOptions.DontRequireReceiver);
            Instantiate(hitFX, hits[i].point, Quaternion.LookRotation(transform.forward));
            Destroy(gameObject);
        }

        RaycastHit[] hits2;
        hits2 = Physics.RaycastAll(transform.position, transform.forward, 25f, hitLayer);
        for (int i = 0; i < hits2.Length; i++)
        {
            hits2[i].transform.SendMessage("AddDamage", damage, SendMessageOptions.DontRequireReceiver);
            Instantiate(hitFX, hits2[i].point, Quaternion.LookRotation(transform.forward));
            Destroy(gameObject);
        }

    }
    private void BulletRay()
    {
        RaycastHit hit1;
        RaycastHit hit2;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit1, 10f, enemyLayer))
        {
            Instantiate(hitFX, hit1.point, Quaternion.LookRotation(hit1.normal));
            hit1.transform.SendMessage("AddDamage", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
        
        // Does the ray intersect any objects excluding the player layer
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit2, 10f, hitLayer))
        {
            Instantiate(hitFX, hit2.point, Quaternion.LookRotation(hit2.normal));
            hit2.transform.SendMessage("AddDamage", damage, SendMessageOptions.DontRequireReceiver);
            GameScore._access.EndHitStreak();
            Destroy(gameObject);
        }

    }
}
