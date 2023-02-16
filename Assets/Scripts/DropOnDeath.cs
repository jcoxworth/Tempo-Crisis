using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDeath : MonoBehaviour
{
    Enemy enemy;
    public GameObject disappearObject;
    public GameObject dropObject;
    public Transform dropFromTransform;

    public bool hasDropped = false;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.onEnemyDie += Drop;
        enemy.onResetEnemy += ResetDrop;
    }
    void ResetDrop()
    {
        hasDropped = false;
        if (disappearObject)
            disappearObject.SetActive(true);
    }
    void Drop()
    {
        if (hasDropped)
            return;

        hasDropped = true;

        if (disappearObject)
            disappearObject.SetActive(false);

        if (dropObject)
        {
            GameObject DroppedObject = Instantiate(dropObject);

            DroppedObject.transform.position = dropFromTransform.position;

            Rigidbody rb = DroppedObject.transform.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddForce(Vector3.up * 3f + Random.onUnitSphere, ForceMode.VelocityChange);
                rb.AddTorque(Random.insideUnitSphere * 10f,ForceMode.VelocityChange);
            }
            Destroy(DroppedObject, 10f);
        }

    }
}
