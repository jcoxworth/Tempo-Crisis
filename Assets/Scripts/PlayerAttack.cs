using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool isActive = true;
    Player player;
    public Weapon weapon;
    public bool isAttacking = false;
    private PlayerHide hide;
    private SquareMove move;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<SquareMove>();
        player = GetComponent<Player>();
        player.onPlayerDie += DisablePlayerAttack;
        player.onResetPlayer += EnablePlayerAttack;
        hide = GetComponent<PlayerHide>();
        weapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._access.isPaused || GameManager._access.isDead || isActive == false)
            return;
        if (move.isMovingSquare)
        {
            weapon.isShooting = false;
            weapon.isActive = false;

            isAttacking = false;
            return;
        }
        else if (hide.isHiding)
        {
            weapon.isShooting = false;
            isAttacking = false;
            weapon.isActive = false;

            weapon.Reload();
            //
            GameScore._access.EndKillStreak();
            return;
        }
        else
        {
            weapon.isActive = true;
        }

        weapon.isShooting = false;
        if (Input.GetMouseButtonDown(0))
            weapon.ShootOne();
        if (Input.GetMouseButton(0))
            weapon.isShooting = true;
        else
            weapon.isShooting = false;
        if (Input.GetMouseButtonUp(0))
            weapon.FastCoolDown();
    }

    private void DisablePlayerAttack()
    {
        isActive = false;
    }
    private void EnablePlayerAttack()
    {
        if (weapon)
            weapon.Reload();
        isActive = true;
    }
}
