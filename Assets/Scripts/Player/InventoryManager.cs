using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour {

	public Weapon[] inventory;
	public int maxInventorySize = 2;
	private WeaponDatabase database;

	void Start(){
		inventory = new Weapon[maxInventorySize];
		database = GameObject.FindGameObjectWithTag ("Weapon Database").GetComponent<WeaponDatabase> ();
	}

	public void AddToInventory(Weapon wep){
		for (int i = 0; i < inventory.Length; i++) {
			if (inventory [i] == null) {
				inventory [i] = wep;
				break;
			}
		}

	}

	public void DropFromInventory(int index){
		inventory [index] = null;
		//TODO Drop?
	}

	public int GetWeaponCount(){
		int count = 0;
		for (int i = 0; i < inventory.Length; i++) {
			if (inventory [i] != null)
				count++;
		}
		return count;
	}

	public WeaponDatabase GetWeaponDatabase(){
		return database;
	}
}
