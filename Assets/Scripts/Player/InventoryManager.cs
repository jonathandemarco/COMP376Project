using UnityEngine;
using System.Collections;

[System.Serializable]
public class InventoryManager : MonoBehaviour {

	public Weapon[] inventory;
	public int maxInventorySize;
	private WeaponDatabase database;
    private Weapon nullWeapon;

	void Start(){
		inventory = new Weapon[maxInventorySize];
		database = GameObject.FindGameObjectWithTag ("Weapon Database").GetComponent<WeaponDatabase> ();
        nullWeapon = (Weapon)Resources.Load("nullWeapon", typeof(Weapon));
    }

	public void AddToInventory(Weapon wep){
		for (int i = 0; i < inventory.Length; i++) {
			if (inventory [i] == null) {
				inventory [i] = wep;
				break;
			}
		}
	}
    public Weapon GetWeapon(int i) {
        if (i > GetWeaponCount())
            return nullWeapon;
        return inventory[i];
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
