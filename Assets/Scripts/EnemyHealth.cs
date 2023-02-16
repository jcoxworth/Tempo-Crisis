using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int originalHealth = 30;
    public int health = 30;
    private Enemy enemy;
    public delegate void OnEnemyDamage();
    public OnEnemyDamage onEnemyDamage;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.onResetEnemy += ResetHealth;

        ResetHealth();
    }

    
    
    public void ReactToDamage()
    {
        onEnemyDamage?.Invoke();
        //EnemyAnimation.DamageAnimation is invoked here
    }
    public void EnemyDie()
    {
        enemy.Die();
    }
    public void ResetHealth()
    {
        health = originalHealth;
    }
}
