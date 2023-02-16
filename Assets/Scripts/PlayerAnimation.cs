using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public bool isActive = true;

    public Transform playerBody;

    Player player;
    Animator animator;
    PlayerHide hide;
    PlayerAttack attack;
    PlayerHealth health;
    SquareMove move;

    public string damageString = "damaged";
    public string deadString = "isDead";
    public string dieString = "dead";
    public string hideString = "isHiding";
    public string hideIntString = "hidingInt";
    public string attackString = "isAttacking";
    public string moveString = "isMoving";
    private int currentHideInt;
    Quaternion targetRotation;
    Vector3 playerBodyTargetPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        player.onPlayerDie += DieAnimation;
        animator = GetComponentInChildren<Animator>();

        hide = GetComponent<PlayerHide>();
        health = GetComponent<PlayerHealth>();
        attack = GetComponent<PlayerAttack>();
        move = GetComponent<SquareMove>();
        health.onDamage += DamageAnimation;
        player.onResetPlayer += ResetAnimation;

    }

    // Update is called once per frame
    void Update()
    {
        //first check if you're alive and don't do anyhting unless you are alive

        if (!player.isAlive)
        {
            
            animator.SetBool(moveString, false);
            animator.SetBool(attackString, false);
            animator.SetBool(hideString, false);
            animator.SetBool(deadString, true); //this keeps it in the dead state until the level is restarted


        }
        currentHideInt = GetCoverTypeInt();

        //next we'll do hiding and moving
        animator.SetBool(attackString, attack.isAttacking);
        animator.SetBool(hideString, hide.isHiding);
        animator.SetInteger(hideIntString, GetCoverTypeInt());
        animator.SetBool(moveString, move.isMovingSquare);

        if (move.isMovingSquare)
        {
            RotateToMovement();
        }
        else
        {
            if (hide.isHiding)
            {
                //when hiding, the position doesn't change, and for vertical cover, when hiding position will be center
                switch (currentHideInt)
                {
                    case 0: //crouching
                        RotateToForwardRight();
                        playerBodyTargetPosition = Vector3.zero;
                        break;
                    case 1: //standing R
                        RotateToBackRight();
                        playerBodyTargetPosition = Vector3.zero;
                        break;
                    case 2: //standing L
                        RotateToBackLeft();
                        playerBodyTargetPosition = Vector3.zero;
                        break;
                    case 4: //no cover
                        RotateToForward();
                        playerBodyTargetPosition = Vector3.zero;
                        break;

                }
            }
            else
            {
                RotateToForward();

                switch (currentHideInt)
                {
                    case 0: //standing from crouching
                        playerBodyTargetPosition = Vector3.zero;
                        break;
                    case 1: //standing R
                        playerBodyTargetPosition = Vector3.right;
                        break;
                    case 2: //standing L
                        playerBodyTargetPosition = Vector3.left;
                        break;
                    case 4: //no cover
                        playerBodyTargetPosition = Vector3.zero;
                        break;

                }
            }

        }

        playerBody.transform.localPosition = Vector3.MoveTowards(playerBody.transform.localPosition, playerBodyTargetPosition, 10f * Time.deltaTime);
        playerBody.transform.rotation = Quaternion.RotateTowards(playerBody.transform.rotation, targetRotation, 5f);

    }
    private int GetCoverTypeInt()
    {
        int i = 3; //3 is an integer that's not used by the animator state machine
        switch (move.currentSquareCovertype)
        {
            case Cover.Covertype.none:
                i = 4;
                break;
            case Cover.Covertype.crouch:
                i = 0;
                break;
            case Cover.Covertype.standR:
                i= 1;
                break;
            case Cover.Covertype.standL:
                i= 2;
                break;
        }
        return i;
    }
    private void DieAnimation()
    {
        if (isActive)
        {
            animator.SetTrigger(dieString);
            isActive = false;
        }

    }
    private void DamageAnimation()
    {
        animator.SetTrigger(damageString);
    }
    private void ResetAnimation()
    {
        Debug.Log("Player anmation reset");
        animator.Play("Player Idle", -1, 0f);
        animator.SetBool(deadString, false);
        animator.SetBool(moveString, false);

        isActive = true;
    }

    private void RotateToMovement()
    {
        Vector3 dirToTgt = move.nextSquare.transform.position - player.transform.position;
        dirToTgt.y = 0f;
        if (dirToTgt != Vector3.zero)
            targetRotation = Quaternion.LookRotation(dirToTgt.normalized, Vector3.up);
    }
    private void RotateToForward()
    {
        targetRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }
    private void RotateToForwardRight()
    {
        targetRotation = Quaternion.LookRotation(Vector3.right +Vector3.forward, Vector3.up);
    }
    private void RotateToBackRight()
    {
        targetRotation = Quaternion.LookRotation(Vector3.right - Vector3.forward, Vector3.up);

    }
    private void RotateToBackLeft()
    {
        targetRotation = Quaternion.LookRotation(Vector3.left - Vector3.forward, Vector3.up);

    }
}
