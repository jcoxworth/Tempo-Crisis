using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Player player;
    public int health = 100;
    public delegate void OnDamage();
    public OnDamage onDamage;
    private void Start()
    {
        player = GetComponent<Player>();
        player.onResetPlayer += ResetPlayerHealth;
        GameManager._access.onRestartLevel += ResetPlayerHealth;
    }
    public void PlayerDie()
    {
        player.PlayerDie();
    }
    public void ReactToDamage()
    {
        onDamage?.Invoke();
    }
    private void ResetPlayerHealth()
    {
        health = 100;
    }
}
