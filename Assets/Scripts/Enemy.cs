using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isAlive = true;
    public bool isPaused = false;
    public delegate void OnEnemyDie();
    public OnEnemyDie onEnemyDie;

    public delegate void OnResetEnemy();
    public OnResetEnemy onResetEnemy;





    public delegate void OnFinishLevel();
    public OnFinishLevel onFinishLevel;

    public delegate void OnPlayerDie();
    public OnPlayerDie onPlayerDie;

    // Start is called before the first frame update
    void Start()
    {
        GameManager._access.onRestartLevel += ResetEnemy;
        GameManager._access.onFinishLevel += FinishLevel;
     //   GameManager._access.onNextLevel += DestroyEnemy;
        GameManager._access.onDieInLevel += PlayerDie;
    }
    private void Update()
    {
        isPaused = GameManager._access.isPaused;
    }
    private void PlayerDie()
    {
        onPlayerDie?.Invoke();
    }
    private void FinishLevel()
    {
        onFinishLevel?.Invoke();
        DestroyEnemy();
    }
    private void ResetEnemy()
    {
        onResetEnemy?.Invoke();
        isAlive = true;
    }

    public void Die()
    {
        //Later we might remove the enemy from some kind of list
        onEnemyDie?.Invoke();

        isAlive = false;
        Destroy(gameObject, 3f);
    }
    public void DestroyEnemy()
    {
       // Destroy(gameObject, 0.1f);
    }
}
