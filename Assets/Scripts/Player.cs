using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isAlive = true;
    public Transform enemyAimAt;
    public delegate void OnPlayerDie();
    public OnPlayerDie onPlayerDie;

    public delegate void OnResetPlayer();
    public OnResetPlayer onResetPlayer;

    private void Awake()
    {
        GameManager._access.onRestartLevel += PlayerReset;
        GameManager._access.onStartLevel += PlayerReset;
    }
    public void PlayerReset()
    {
        onResetPlayer?.Invoke();
        isAlive = true;
    }
    public void PlayerDie()
    {
        isAlive = false;
        onPlayerDie?.Invoke();
        GameManager._access.DieInLevel();
    }
}
