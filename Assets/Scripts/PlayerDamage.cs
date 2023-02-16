using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    PlayerHealth health;
    void Start()
    {
        health = GetComponentInParent<PlayerHealth>();
    }

    public void AddDamage(int amount)
    {
        health.health -= amount;
        if (health.health < 1)
            health.PlayerDie();
        else
            health.ReactToDamage();
    }
}
