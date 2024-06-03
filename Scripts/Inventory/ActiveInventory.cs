using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : Singleton<ActiveInventory>
{
    [SerializeField] private int activeSlotIndexNum = 0;

    protected override void Awake()
    {
        base.Awake();
    }
    public void EquipStartingWeapon()
    {
        ToggleActiveHighlight(0);
    }

    private void Update() {
    #if UNITY_EDITOR
        if (Input.GetButtonDown("Cancel"))
        {
            if(activeSlotIndexNum == 2)
            {
                activeSlotIndexNum = 0;
            }
            else
            {
                activeSlotIndexNum = activeSlotIndexNum+1 ;
            }
            ToggleActiveSlot(this.activeSlotIndexNum);
        }

    #else
       if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
           
            if(activeSlotIndexNum == 2)
            {
                activeSlotIndexNum = 0;
            }
            else
            {
                activeSlotIndexNum = activeSlotIndexNum+1 ;
            }
            
            ToggleActiveSlot(this.activeSlotIndexNum);
        }
    #endif
    }


    private void ToggleActiveSlot(int numValue) {
        ToggleActiveHighlight(numValue);
    }

    private void ToggleActiveHighlight(int indexNum) {
      activeSlotIndexNum = indexNum;
        
        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;

        if (weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }


        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform);
        //GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        //ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        //newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }

}

