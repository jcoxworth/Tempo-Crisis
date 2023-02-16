using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    public int objectHealth = 20;

    private Vector3 startPosition;
    private Quaternion startRotation;
    public CoverObjects coverObjects;

    Rigidbody rb;
    Collider col;
    private bool isReactingToDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        startPosition= transform.localPosition;
        startRotation = transform.localRotation;
        coverObjects = GetComponentInParent<CoverObjects>();


        coverObjects.onScatter += Scatter;
        coverObjects.onResetObjects += ResetObject;

        ResetObject();
    }
    public void ResetObject()
    {
        gameObject.SetActive(true);

        transform.localPosition = startPosition;
        transform.localRotation = startRotation;
        col.enabled = true;
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public void Scatter()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(2f * Vector3.up + Random.onUnitSphere * 3f, ForceMode.Impulse);
        rb.AddTorque(10f * Random.onUnitSphere, ForceMode.Impulse);
    }

    public void DestroyObject()
    {
        col.enabled = false;
        rb.isKinematic = true;
        rb.useGravity = false;

        gameObject.SetActive(false);
    }
    public void AddDamage(int amount)
    {
        objectHealth -= amount;
        StartCoroutine(ReactToDamage());
    }
    private void DamageShake()
    {
        if (isReactingToDamage)
            transform.localPosition = startPosition + Random.onUnitSphere * 0.01f;
        else
            transform.localPosition = startPosition;
    }
    private IEnumerator ReactToDamage()
    {
        isReactingToDamage = true;
        yield return new WaitForSeconds(0.1f);

        isReactingToDamage = false;
        if (objectHealth < 1)
            DestroyObject();
    }
}
