using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFX : MonoBehaviour
{
    ExtraObject extra;
    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        extra = GetComponent<ExtraObject>();

        extra.onDestroy += DestroyEffect;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyEffect()
    {
        Instantiate(effect, transform.position, transform.rotation, transform);
    }
}
