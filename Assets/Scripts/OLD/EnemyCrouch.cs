using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrouch : MonoBehaviour
{
    public bool isActive = true;
    public bool isCrouching = true;
    public GameObject enemyCapsule;
    private Vector3 targetScale;
    private Vector3 targetPosition;
    public EnemyHealth health;

    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.onEnemyDie += DisableCrouching;
        enemy.onResetEnemy += ResetCrouching;
        ResetCrouching();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;
        if (GameManager._access.isPaused)
            return;
        
        if (isCrouching)
        {
            enemyCapsule.transform.localScale = new Vector3(1f, 0.5f, 1f);
            enemyCapsule.transform.localPosition = new Vector3(0f, 0.5f, 0f);
        }
        else
        {
            enemyCapsule.transform.localScale = new Vector3(1f, 1f, 1f);
            enemyCapsule.transform.localPosition = new Vector3(0f, 1f, 0f);
        }

    }
    private void ResetCrouching()
    {
        isActive = true;
        isCrouching = false;

    }
    private void DisableCrouching()
    {
        isCrouching = false;
        isActive = false;
    }
}
