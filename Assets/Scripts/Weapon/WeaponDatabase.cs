using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDatabase : MonoBehaviour {
	public static WeaponDatabase currentWeaponDatabase;
	public List<Weapon> weaponDatabase = new List<Weapon>();

    public Weapon nullWeapon;

	public void Start()
	{
		currentWeaponDatabase = this;
	}
    public Weapon getNullWeapon() {
        return nullWeapon;
    }
	public Weapon GetWeaponAt(int index){
		Weapon foundWeapon = weaponDatabase [index];
		return foundWeapon;
	}

}
