using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public bool isActive = true;
    public bool isShooting = false;
    public bool isDamagePause = false;
    public float damagePauseTime = 1.5f;
    public Transform gunRig;
    public Transform gunBarrel;

    private float timeStamp;
    public float shotCoolDown = 0.5f;
    public int currentShots = 0;
    public int shotsBeforeHitPlayer = 3;

    public GameObject bullet;

    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();

        enemy.onResetEnemy += ResetShooting;
        enemy.onEnemyDie += DisableShooting;

        // health.onEnemyDamage += StartDamagePause;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;
        if (GameManager._access.isPaused)
            return;
        
        if (isDamagePause)
        {
            timeStamp += Time.deltaTime;
            if (timeStamp > damagePauseTime)
                isDamagePause = false;
            return;
        }


        if (isShooting)
            Shooting();

        
    }
    private void StartDamagePause()
    {
        timeStamp = 0f;
        isShooting = false;
        isShooting = false;
        isDamagePause = true;
    }
    private void DisableShooting()
    {
        isActive = false;
        isShooting = false;
    }
    private void Shooting()
    {
        timeStamp += Time.deltaTime;

        if (timeStamp > shotCoolDown)
        {
            ShootOne();
        }
    }
    private void ShootOne()
    {
        GameObject b = Instantiate(bullet, gunBarrel.position, gunBarrel.rotation);
        currentShots++;
        AimGunRig();
        if (currentShots > shotsBeforeHitPlayer)
            currentShots = 0;

        timeStamp = 0;
    }

    private void AimGunRig()
    {
        if (currentShots == shotsBeforeHitPlayer)
        {
            Vector3 directionToPlayer = GameManager._access.player.transform.position - gunRig.transform.position;
            gunRig.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
        }
        else
        {
            Vector3 RandomPoint = GameManager._access.player.transform.position + Random.onUnitSphere * 1.5f;
            RandomPoint.y = Mathf.Abs( RandomPoint.y);
            Vector3 missingDirection = RandomPoint - gunRig.transform.position;
            gunRig.rotation = Quaternion.LookRotation(missingDirection, Vector3.up);
        }
    }
    public void ResetShooting()
    {
        isActive = true;
        isDamagePause = false;
        isShooting = false;
        timeStamp = 0;
    }
}
