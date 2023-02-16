using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public bool isActive = true;
    Player player;
    SquareMove move;

    public Vector3 aimPoint;
    public Transform gunRig;
    public Transform cameraTransform;
    public CameraLook camLook;
    public CameraZoom camZoom;
    public float normalSensitivity = 15f;
    public float zoomedSensitivity = 6f;
    public float helpSensitivity = 1f;

    public LayerMask enemyLayer;

    private Vector3 gunRigOriginalLocalPos;

    public bool isAimingDownSights = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        move = GetComponent<SquareMove>();
        player.onPlayerDie += DisableAiming;
        player.onResetPlayer += ResetAiming;

        gunRigOriginalLocalPos = gunRig.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;

        AimDownSightsControl();
        AimHelping();
        AimingDownSights();
        LookSensitivity();
        aimPoint = GetAimPoint();
        Vector3 aimDirection = aimPoint - gunRig.transform.position;
        gunRig.rotation = Quaternion.LookRotation(aimDirection, Vector3.up);
    }
    private void AimDownSightsControl()
    {
        isAimingDownSights = Input.GetMouseButton(1) && !move.isMovingSquare;

    }
    private void AimingDownSights()
    {
        camZoom.isZooming = isAimingDownSights;

        if (isAimingDownSights)
        {
            gunRig.position = cameraTransform.position;
        }
        else
        {
            gunRig.localPosition = gunRigOriginalLocalPos;

        }
    }
    private void LookSensitivity()
    {
        float sensitivity = 1f;
        if (Input.GetMouseButton(1))
            sensitivity = zoomedSensitivity;
        else
            sensitivity = normalSensitivity;
        sensitivity *= helpSensitivity;

        camLook.sensitivity = sensitivity;
    }
    private void AimHelping()
    {
        Vector3 origin = cameraTransform.position + (cameraTransform.forward * 0.5f);
        Vector3 direction = cameraTransform.TransformDirection(Vector3.forward);
        Ray r = new Ray(origin, direction);
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer

        if (Physics.SphereCast(r, 0.2f, out hit, 100f, enemyLayer))
        {
            helpSensitivity = 0.3f;
        }
        else
        {
            helpSensitivity = 1f;
        }
    }
    private Vector3 GetAimPoint()
    {
        Vector3 ap = Vector3.zero;
        Vector3 origin = cameraTransform.position + (cameraTransform.forward * 0.5f);
        Vector3 direction = cameraTransform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer

        if (Physics.Raycast(origin, direction, out hit, Mathf.Infinity))
        {
            ap = hit.point;
        }
        else
        {
            ap = cameraTransform.position + (cameraTransform.forward * 100f);
        }

        return ap;
    }

    private void DisableAiming()
    {
        isActive = false;
    }
    private void ResetAiming()
    {
        isActive = true;
    }
}
