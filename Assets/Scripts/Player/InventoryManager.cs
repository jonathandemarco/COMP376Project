using UnityEngine;
using System.Collections;

[System.Serializable]
public class InventoryManager : MonoBehaviour {

	public Weapon[] inventory;
	public int maxInventorySize;
	private WeaponDatabase database;
    public Weapon nullWeapon;

	void Start(){
		inventory = new Weapon[maxInventorySize];
		database = GameObject.FindGameObjectWithTag ("Weapon Database").GetComponent<WeaponDatabase> ();
        nullWeapon = database.getNullWeapon();
        for (int i = 0; i < maxInventorySize; i++) {
            inventory[i] = nullWeapon;
        }
    }

	public void AddToInventory(Weapon wep){
		for (int i = 0; i < inventory.Length; i++) {
			if (inventory [i] == nullWeapon) {
				inventory [i] = wep;
				break;
			}
		}
	}
    public Weapon GetWeapon(int i) {
        if (i > GetWeaponCount())
        {
            return nullWeapon;
        }
        return inventory[i];
    }

    public Weapon[] getWeaponList() {
        return inventory;
    }
	public void DropFromInventory(int index){
		inventory [index] = nullWeapon;
		//TODO Drop?
	}

	public int GetWeaponCount(){
		int count = 0;
		for (int i = 0; i < inventory.Length; i++) {
			if (inventory [i] != nullWeapon)
				count++;
		}
		return count;
	}

	public WeaponDatabase GetWeaponDatabase(){
		return database;
	}
}
