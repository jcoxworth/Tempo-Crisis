using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiming : MonoBehaviour
{
    public bool isActive = true;
    Enemy enemy;

    public Transform gunRig;

    public bool isAimingDirectlyAtPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();

        enemy.onResetEnemy += ResetAiming;
        enemy.onEnemyDie += DisableAiming;

    }

    private void Update()
    {
        Debug.DrawRay(gunRig.position, gunRig.forward *100f, Color.red);
    }
    public void AimGunRig()
    {
        if (!isActive)
            return;

        if (isAimingDirectlyAtPlayer)
        {
            Vector3 directionToPlayer = GameManager._access.player.enemyAimAt.position - gunRig.transform.position;
            gunRig.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
        }
        else
        {
            Vector3 RandomPoint = GameManager._access.player.transform.position + Random.onUnitSphere * 1.5f;
            RandomPoint.y = Mathf.Abs(RandomPoint.y);
            Vector3 missingDirection = RandomPoint - gunRig.transform.position;
            gunRig.rotation = Quaternion.LookRotation(missingDirection, Vector3.up);
        }
    }
    private void DisableAiming()
    {
        isActive = false;

    }
    private void ResetAiming()
    {
        isActive = true;
    }
}
