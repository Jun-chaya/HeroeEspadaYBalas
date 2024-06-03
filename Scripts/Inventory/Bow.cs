using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    
    
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private Animator myAnimator;
    private string facing;

   
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        ActiveWeapon.Instance.transform.rotation = Quaternion.identity;
    }

    public void Attack(string facing)
    {

        this.facing = facing;
     
        if (facing== "LEFT")
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
        if(facing == "DOWN")
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        myAnimator.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);

    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}

