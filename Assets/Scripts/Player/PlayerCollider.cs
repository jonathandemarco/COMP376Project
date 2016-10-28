using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

    public InventoryManager inventoryManager;
    
    void OnTriggerEnter(Collider col){
        Debug.Log("PCIKUP");
		if (col.gameObject.name == "Crate") {

			if (inventoryManager.GetWeaponCount() < inventoryManager.maxInventorySize) {
				//add the item to player's inventory
				int id = col.GetComponent<Crate> ().IDValue;
				WeaponDatabase database = inventoryManager.GetWeaponDatabase ();
				Weapon weaponToAdd = database.GetComponent<WeaponDatabase> ().GetWeaponAt (id); 
				inventoryManager.AddToInventory (weaponToAdd);
				Weapon w = (Weapon) Instantiate (weaponToAdd, transform.parent);
                w.transform.parent = transform;


                Destroy(col.gameObject);
			}
		}
	}
}
