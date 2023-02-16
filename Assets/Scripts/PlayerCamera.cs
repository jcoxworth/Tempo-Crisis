using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform cameraRigTransform;
    public Transform stationary, crouching, hidingStandR, hidingStandL, moving, dead;
    private Transform targetTransform;
    private Vector3 targetPosition;
    private SquareMove squareMove;
    private PlayerHide hide;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        hide = GetComponent<PlayerHide>();
        squareMove = GetComponent<SquareMove>();

        GameManager._access.onNextLevel += ResetCamera;
        GameManager._access.onRestartLevel += ResetCamera;
    }
    void ResetCamera()
    {
        cameraRigTransform.position = targetPosition;
        cameraRigTransform.rotation = Quaternion.identity;
    }
    // Update is called once per frame
    void Update()
    {
        if (!squareMove)
            return;
        if (!player.isAlive)
        {
           // cameraRigTransform.position = dead.position;
            cameraRigTransform.position = Vector3.Lerp(cameraRigTransform.position, dead.position, Time.deltaTime * 7.5f);

            //cameraRigTransform.rotation = dead.rotation;
            cameraRigTransform.rotation = Quaternion.Lerp(cameraRigTransform.rotation, dead.rotation, Time.deltaTime * 7.5f);
            return;
        }


        if (squareMove.isMovingSquare)
        {
            targetTransform = moving;
        }
        else
        {
            if (hide.isHiding)
            {
               switch (hide.currentCoverType)
                {
                    case Cover.Covertype.crouch:
                        targetTransform = crouching;
                        break;
                    case Cover.Covertype.standR:
                        targetTransform = hidingStandR;
                        break;
                    case Cover.Covertype.standL:
                        targetTransform = hidingStandL;
                        break;
                }
            }
            else
                targetTransform = stationary;
        }

        if (targetTransform)
            cameraRigTransform.position = Vector3.Lerp(cameraRigTransform.position, targetTransform.position, Time.deltaTime * 7.5f);
    }
}
