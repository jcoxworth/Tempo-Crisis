using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool isActive = true;
    public bool isAttacking = false;
    public bool isDamagePause = false;
    public float damagePauseTime = 1.5f;
    private float timeStamp;

    public int attackCount = 0;
    public int attacksBeforeHittingPlayer = 3;

    Enemy enemy;
    Weapon weapon;
    EnemyAiming aiming;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.onResetEnemy += ResetAttacking;
        enemy.onEnemyDie += DisableAttacking;


        aiming = GetComponent<EnemyAiming>();
        weapon = GetComponentInChildren<Weapon>();
        weapon.onShoot += CountAttack;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive || enemy.isPaused)
        {
            isAttacking = false;
            weapon.isShooting = false;
            return;
        }
        if (GameManager._access.isPaused)
            return;

        if (isDamagePause)
        {
            DisableAttacking();
            timeStamp += Time.deltaTime;
            if (timeStamp > damagePauseTime)
                isDamagePause = false;
            return;
        }

        if(weapon)
            weapon.isShooting = isAttacking;

        if (aiming)
            aiming.isAimingDirectlyAtPlayer = attacksBeforeHittingPlayer == attackCount;

    }
    private void CountAttack()
    {
        aiming.AimGunRig();
        attackCount++;
        if (attackCount > attacksBeforeHittingPlayer)
            attackCount = 0;
    }

    private void DisableAttacking()
    {
        weapon.isActive = false;
        isActive = false;
        isAttacking = false;
    }
    private void ResetAttacking()
    {
        attackCount = 0;
        weapon.isActive = true;
        isActive = true;
        isDamagePause = false;
        isAttacking = false;
        timeStamp = 0;
    }
}
