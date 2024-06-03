using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour,IWeapon {
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform magicLaserSpawnPoint;

    private Animator myAnimator;
    private string facing;
    private Transform weaponCollider;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }


    public void Attack(string facing)
    {
        this.facing = facing;

        if (facing == "LEFT")
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        if (facing == "RIGHT")
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (facing == "UP")
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (facing == "DOWN")
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        Attack();
    }

    public void Attack()
    {
        myAnimator.SetTrigger(ATTACK_HASH);

    }

    public void SpawnStaffProjectileAnimEvent()
    {
        GameObject newLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, Quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);

        newLaser.GetComponent<MagicLaser>().setFacing(this.facing);

    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;

    }
}
