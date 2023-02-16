using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    Enemy enemy;
    EnemyHealth health;
    public AudioSource audioSource;

    public AudioClip[] damage;
    public AudioClip[] die;
    private bool canDie = true;
    // Start is called before the first frame update
    void Start()
    {
        canDie = true;
        enemy = GetComponent<Enemy>();
        health = GetComponent<EnemyHealth>();

        enemy.onEnemyDie += PlayDie;
        health.onEnemyDamage += PlayDamage;
    }
    void PlayDamage()
    {
        audioSource.Stop();

        if (enemy.isAlive)
            audioSource.PlayOneShot(damage[GameManager._access.Cycle8()]);
    }

    void PlayDie()
    {
        if (canDie)
        {
            audioSource.Stop();
            float r = Random.value;
            if (r < 0.3f)
                audioSource.PlayOneShot(die[GameManager._access.Cycle8()]);
            else
                audioSource.PlayOneShot(damage[GameManager._access.Cycle8()]);

            canDie = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
