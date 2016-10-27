using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {


	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "Crate") {
			InventoryManager inventoryManager = transform.parent.GetComponent<InventoryManager> ();
			if (inventoryManager.GetWeaponCount() < inventoryManager.maxInventorySize) {
				//add the item to player's inventory
				int id = col.GetComponent<Crate> ().IDValue;
				WeaponDatabase database = inventoryManager.GetWeaponDatabase ();
				Weapon weaponToAdd = database.GetComponent<WeaponDatabase> ().GetWeaponAt (id); 
				inventoryManager.AddToInventory (weaponToAdd);
				Instantiate (weaponToAdd, transform.parent);
				Destroy (col.gameObject);
			}
		}
	}
}
