using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public bool isActive = true;
    public float totalTime = 7f;
    public float shootTime = 5f;
    public float standTime = 3f;
    public float hideTime = 2f;
    public float stand2Time = 1f;

    public EnemyHealth health;
    public EnemyMove move;
    public EnemyHide hide;
    public EnemyAttack attack;


    private float t;
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {

        enemy = GetComponent<Enemy>();

        enemy.onEnemyDie += StopBehavior;
        enemy.onResetEnemy += RestartBehavior;
        enemy.onFinishLevel += StopBehavior;

        health = GetComponent<EnemyHealth>();
        health.onEnemyDamage += DamagePause; 
        move = GetComponent<EnemyMove>();
        RestartBehavior();

    }
    private void OnEnable()
    {
        RestartBehavior();
    }
    private void Update()
    {
        if (enemy.isPaused || !enemy.isAlive)
            return;
        
        if (move.isMoving)
        {
            attack.isAttacking = false;
            hide.isHiding = false;
            return;
        }

        BehaviorLoop();
    }
    private void DamagePause()
    {
        t = shootTime - 0.1f;
    }
    private void BehaviorLoop()
    {
        t -= Time.deltaTime;

        if (t > shootTime) //and less than 7f
        {
            //Attack
            attack.isAttacking = true;
            hide.isHiding = false;
        }
        else if (t < shootTime && t >standTime)
        {
            //Stand
            attack.isAttacking = false;
            hide.isHiding = false;
        }
        else if (t < standTime && t > hideTime)
        {
            //Hide
            attack.isAttacking = false;
            hide.isHiding = true;
        }
        else if (t < hideTime && t > stand2Time)
        {
            //Stand
            attack.isAttacking = false;
            hide.isHiding = false;
        }
        else
        {
            t += totalTime;
        }
        
    }

    private void RestartBehavior()
    {
        t = Random.Range(0.5f, 6.5f);
        isActive = true;
    }
    private void StopBehavior()
    {
        isActive = false;
    }
}
