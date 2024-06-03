using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
   
    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    private float timeBetweenAttacks;
    private bool attackButtonDown, isAttacking = false;
    private string lastFacing; 

    private void Start()
    {
        isAttacking = false;
        attackButtonDown = false;
    }
    protected override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
#if UNITY_EDITOR
        

        if (Input.GetButtonDown("Fire1"))
        {
            this.lastFacing = "RIGHT";
          
            StartAttacking();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopAttacking();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            this.lastFacing = "LEFT";
            StartAttacking();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            StopAttacking();
        }

        if (Input.GetButtonDown("Fire3"))
        {
            this.lastFacing = "DOWN";
            StartAttacking();
        }
        if (Input.GetButtonUp("Fire3"))
        {
            StopAttacking();
        }
        if (Input.GetButtonDown("Jump"))
        {
            this.lastFacing = "UP";
            StartAttacking();
        }
        if (Input.GetButtonUp("Jump"))
        {
            StopAttacking();
        }
        Attack();

#else

        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
             this.lastFacing = "RIGHT";
            StartAttacking();
        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button1))
        {
            StopAttacking();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            this.lastFacing = "LEFT";
            StartAttacking();
        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button2))
        {
            StopAttacking();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
             this.lastFacing = "DOWN";
            StartAttacking();
           
        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button0))
        {
            StopAttacking();
        }


        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            this.lastFacing = "UP";
            StartAttacking();
        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button3))
        {
            StopAttacking();
        }
        Attack();
#endif

    }
    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
        AttackCooldown();
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }
 

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void Attack()
    {
        if (attackButtonDown && !isAttacking && CurrentActiveWeapon)
        {
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon).Attack(this.lastFacing);
        }
    }
}
