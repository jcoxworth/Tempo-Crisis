using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
        public float sensitivity = 15F;
        public float minimumX = -360F;
        public float maximumX = 360F;
        public float minimumY = -60F;
        public float maximumY = 60F;
        float rotationX = 0F;
        float rotationY = 0F;
        Quaternion originalRotation;
    public Transform cameraTransform;
    void Start()
    {

        originalRotation = cameraTransform.localRotation;
    }
    void Update()
    {
        if (GameManager._access.isPaused || GameManager._access.isDead)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else
        {
            // Read the mouse input axis
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.SetCursor(null, Vector2.zero, cursorMode);

            rotationX += Input.GetAxis("Mouse X") * sensitivity;
            rotationY += Input.GetAxis("Mouse Y") * sensitivity;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            cameraTransform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
           
    }
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    
    public static float ClampAngle(float angle, float min, float max)
    {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
    }
}
