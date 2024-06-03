using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IWeapon {

    void Attack(string facing);
    WeaponInfo GetWeaponInfo();
}
