using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraObject : MonoBehaviour
{
    public int objectHealth = 20;

    Rigidbody rb;
    Collider col;
    private bool isReactingToDamage = false;
    private Vector3 startPosition;

    public delegate void OnDestroy();
    public OnDestroy onDestroy;

    public delegate void OnDamage();
    public OnDamage onDamage;
    private MeshRenderer renderer;

    public float shakeAmount = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        startPosition = transform.localPosition;
        renderer = GetComponent<MeshRenderer>();
        GameManager._access.onRestartLevel += ResetObject;

        ResetObject();
    }

    public void AddDamage(int amount)
    {
        objectHealth -= amount;
        StartCoroutine(ReactToDamage());
        onDamage?.Invoke();
    }

    public void ResetObject()
    {
        renderer.enabled = true;
        
        col.enabled = true;
    }
    public void DestroyObject()
    {
        renderer.enabled = false;
        col.enabled = false;
        onDestroy?.Invoke();
    }
    private void DamageShake()
    {
        if (isReactingToDamage)
            transform.localPosition = startPosition + Random.onUnitSphere * shakeAmount;
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
