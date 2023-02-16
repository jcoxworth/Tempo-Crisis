using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Range(1, 5)]
    public int multiplier = 1;

    //this is just for connecting to the root enemyHealth.cs and messaging damage events
    //later we can add different body parts with different damage multipliers

    public EnemyHealth health;
    void Start()
    {
        health = GetComponentInParent<EnemyHealth>();
    }

    public void AddDamage(int amount)
    {
        if (health.health > 0)
            GameScore._access.IncrementHitStreak();



        health.health -= amount * multiplier;
        if (health.health < 1)
        {
            health.EnemyDie();
            GameScore._access.IncrementKillStreak();
        }
        else
        {
            health.ReactToDamage();
        }
    }
}
