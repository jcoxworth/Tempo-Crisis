using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHide : MonoBehaviour
{
    public bool isActive = true;
    public bool isHiding = true;
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.onEnemyDie += DisableHiding;
        enemy.onResetEnemy += ResetHiding;
        ResetHiding();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;
        if (GameManager._access.isPaused)
            return;

    }
    
    private void ResetHiding()
    {
        isActive = true;
        isHiding = false;

    }
    private void DisableHiding()
    {
        isHiding = false;
        isActive = false;
    }
}
