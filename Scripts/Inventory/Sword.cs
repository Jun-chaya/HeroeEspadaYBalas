using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;

public class Sword : MonoBehaviour , IWeapon
{

    [SerializeField] private GameObject slashAnimationPrefab;
    [SerializeField] private float swordAttackCD = .5f;
    [SerializeField] private WeaponInfo weaponInfo;

    private Transform slashAnimationSpawnPoint;
    private Animator myAnimator;
    private Transform weaponCollider;

    private string facing;
    
    private GameObject slashAnimation;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        weaponCollider = MovimientoTopDown.Instance.getWeaponCollider();
        slashAnimationSpawnPoint = GameObject.Find("SlashAnimationSpawnPoint").transform;
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void SwingUpFlipAnimEvent()
    {
     
        if (facing == "LEFT") {
            slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
        if(facing == "RIGHT")
        {
            slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
            slashAnimation.GetComponent <SpriteRenderer>().flipX = false;
        }
        if (facing == "DOWN")
        {
            slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (facing == "UP")
        {

            slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            slashAnimation.GetComponent<SpriteRenderer>().flipY = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
       
        if (facing == "LEFT")
        {
            slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
            
        }
        if (facing == "RIGHT")
        {
            slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            slashAnimation.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (facing == "DOWN")
        {

            slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        if (facing == "UP")
        {
            slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            slashAnimation.GetComponent<SpriteRenderer>().flipY = true;
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
           

        }
    } 

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }


    public void Attack(string facing)
    {

        this.facing = facing;
     
        if (facing== "LEFT")
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        if (facing == "RIGHT")
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        if (facing == "UP")
        {
            //this.GetComponent<SpriteRenderer>().sortingOrder = -1;
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 90);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if(facing == "DOWN")
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, -90);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, -90);
        }

            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            slashAnimation = Instantiate(slashAnimationPrefab, slashAnimationSpawnPoint.position, Quaternion.identity);
            slashAnimation.transform.parent = this.transform.parent;
        }
       
}
