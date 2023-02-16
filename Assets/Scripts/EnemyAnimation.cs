using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public bool isActive = true;
    Enemy enemy;
    public Animator animator;
    EnemyHide hide;
    EnemyAttack attack;
    EnemyHealth health;
    EnemyMove move;
    Weapon weapon;

    public string damageString = "damaged";
    public string deadString = "isDead";
    public string dieString = "dead";
    public string hideString = "isHiding";
    public string hideIntString = "hidingInt";
    public string attackString = "isAttacking";
    public string moveString = "isMoving";
    public string damageIntString = "damageInt";

    public float attackWeight;
    public float damageWeight;
    public float recoilWeight;

    public Transform enemyBody;

    public int currentHideInt;
    Quaternion enemyBodyTargetRotation;
    Vector3 enemyBodyTargetPosition;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        hide = GetComponent<EnemyHide>();
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<EnemyHealth>();
        attack = GetComponent<EnemyAttack>();
        move = GetComponent<EnemyMove>();
        health.onEnemyDamage += DamageAnimation;
        enemy.onEnemyDie += DieAnimation;
        enemy.onResetEnemy += ResetAnimation;

        weapon = GetComponentInChildren<Weapon>();
        weapon.onShoot += Recoil;
    }

    // Update is called once per frame
    void Update()
    {
        //first check if you're alive and don't do anyhting unless you are alive
        Weights();

        if (!enemy.isAlive)
        {
            AnimatorParametersDead();
            return;
        }


        //next we'll do hiding and moving


        AnimatorParameters();
        
        
        if (move && move.isMoving)
        {
            enemyBodyTargetPosition = Vector3.zero;
            Movement();
            MoveRotateEnemyBody();
            return;
        }

        if (hide.isHiding)
        {
            Hiding();
            MoveRotateEnemyBody();
            return;
        }

        NotHiding();
        MoveRotateEnemyBody();



    }
    private void AnimatorParametersDead()
    {
        animator.SetBool(moveString, false);
        animator.SetBool(attackString, false);
        animator.SetBool(hideString, false);
        animator.SetBool(deadString, true); //this keeps it in the dead state until the level is restarted
    }
    private void AnimatorParameters()
    {
        animator.SetBool(moveString, move.isMoving);
        if(attack)
            animator.SetBool(attackString, attack.isAttacking);
        animator.SetBool(hideString, hide.isHiding);
        animator.SetInteger(hideIntString, GetCoverTypeInt());
        currentHideInt = GetCoverTypeInt();
    }
    private void Weights()
    {
        if (attack && attack.isAttacking)
            attackWeight = Mathf.MoveTowards(attackWeight, 1f, 0.03f);
        else
            attackWeight = Mathf.MoveTowards(attackWeight, 0f, 0.03f);


        damageWeight = Mathf.MoveTowards(damageWeight, 0f, 0.0125f);
        recoilWeight = Mathf.MoveTowards(recoilWeight, 0f, 0.05f);
        animator.SetLayerWeight(1, attackWeight);
        animator.SetLayerWeight(2, damageWeight);
        animator.SetLayerWeight(3, recoilWeight);
    }
    private void Movement()
    {
        Vector3 dirToTgt = move.targetPosition - enemy.transform.position;
        dirToTgt.y = 0f;
        if (dirToTgt != Vector3.zero)
            enemyBodyTargetRotation = Quaternion.LookRotation(dirToTgt.normalized, Vector3.up);
    }
    private void Hiding()
    {
        //when hiding, the position doesn't change, and for vertical cover, when hiding position will be center
        switch (currentHideInt)
        {
            case 0: //crouching
                enemyBodyTargetPosition = Vector3.zero;
                RotateToForwardRight();
                break;
            case 1: //standing R
                enemyBodyTargetPosition = Vector3.zero;
                RotateToBackRight();
                break;
            case 2: //standing L
                enemyBodyTargetPosition = Vector3.zero;
                RotateToBackLeft();
                break;
            case 4: //no cover
                enemyBodyTargetPosition = Vector3.zero;
                break;
        }
        
    }
    private void NotHiding()
    {
        RotateToPlayer();
        //when hiding, the position doesn't change, and for vertical cover, when hiding position will be center
        switch (currentHideInt)
        {
            case 0: //crouching
                enemyBodyTargetPosition = Vector3.zero;
                break;
            case 1: //standing R
                enemyBodyTargetPosition = Vector3.forward + Vector3.left;
                break;
            case 2: //standing L
                enemyBodyTargetPosition = Vector3.forward + Vector3.right;
                break;
            case 4: //no cover
                enemyBodyTargetPosition = Vector3.zero;
                break;
        }
    }
    private void MoveRotateEnemyBody()
    {
        enemyBody.transform.localPosition = Vector3.MoveTowards(enemyBody.transform.localPosition, enemyBodyTargetPosition, 10f * Time.deltaTime);
        enemyBody.transform.rotation = Quaternion.RotateTowards(enemyBody.transform.rotation, enemyBodyTargetRotation, 5f);
    }
    private int GetCoverTypeInt()
    {
        int i = 3; //3 is an integer that's not used by the animator state machine
        if (!move.currentCoverSquare)
            return i;

        switch (move.currentCoverSquare.GetCoverType())
        {
            case Cover.Covertype.none:
                i = 4;
                break;
            case Cover.Covertype.crouch:
                i = 0;
                break;
            case Cover.Covertype.standR:
                i = 1;
                break;
            case Cover.Covertype.standL:
                i = 2;
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
    private int damageInt = 0;
    private void DamageAnimation()
    {
        damageInt = Random.Range(0, 7);

        animator.SetInteger(damageIntString, damageInt);
        damageWeight = 1f;
        animator.SetTrigger(damageString);
    }
    private void Recoil()
    {
        recoilWeight = 1f;
    }
    private void ResetAnimation()
    {
        if (!animator)
            return;
        animator.SetBool(deadString, false);
        isActive = true;
    }

    //Enemy's forward is Vector3.back
    private void RotateToForward()
    {
        enemyBodyTargetRotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
    }
    private void RotateToForwardRight()
    {
        enemyBodyTargetRotation = Quaternion.LookRotation(Vector3.right + Vector3.back, Vector3.up);
    }

    private void RotateToBackRight()
    {
        enemyBodyTargetRotation = Quaternion.LookRotation(Vector3.right - Vector3.back, Vector3.up);

    }
    private void RotateToBackLeft()
    {
        enemyBodyTargetRotation = Quaternion.LookRotation(Vector3.left - Vector3.back, Vector3.up);

    }

    
    private void RotateToPlayer()
    {
        Vector3 dirToPlayer = GameManager._access.player.transform.position - enemyBody.transform.position;
        dirToPlayer.y = 0f;
        if (dirToPlayer.normalized != Vector3.zero)
            enemyBodyTargetRotation = Quaternion.LookRotation(dirToPlayer.normalized, Vector3.up);
    }
}
