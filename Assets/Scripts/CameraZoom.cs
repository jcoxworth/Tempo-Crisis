using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    CameraLook cameraLook;
    Camera cam;

    public bool isZooming = false;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isZooming)
        {
            cam.fieldOfView = 30f;
        }
        else
        {
            cam.fieldOfView = 60f;
        }
    }
}
