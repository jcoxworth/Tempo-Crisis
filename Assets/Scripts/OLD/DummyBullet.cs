using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBullet : MonoBehaviour
{

    private Rigidbody rb;
    public float speed = 200f;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime));
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
