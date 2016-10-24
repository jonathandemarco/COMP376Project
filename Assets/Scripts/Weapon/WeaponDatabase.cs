using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDatabase : MonoBehaviour {

	public List<Weapon> weaponDatabase = new List<Weapon>();

	public Weapon GetWeaponAt(int index){
		Weapon foundWeapon = weaponDatabase [index];
		return foundWeapon;
	}

}
