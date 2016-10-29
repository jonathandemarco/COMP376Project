using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

    private InventoryManager inventoryManager;

    void Start() {
        inventoryManager = GetComponentInChildren<InventoryManager>();
    }
    void OnTriggerEnter(Collider col){
		if (col.gameObject.layer == LayerMask.NameToLayer("Crate")) {
            Debug.Log("Picked up weapon!");
			if (inventoryManager.GetWeaponCount() < inventoryManager.maxInventorySize) {
				//add the item to player's inventory
				int id = col.GetComponent<Crate> ().IDValue;
				WeaponDatabase database = inventoryManager.GetWeaponDatabase ();
				Weapon weaponToAdd = database.GetComponent<WeaponDatabase> ().GetWeaponAt (id); 
				inventoryManager.AddToInventory (weaponToAdd);

				/* 
				 * I hope this was just to test... I added the code in PlayerManager
				 * Instantiate the weapon once a button is pressed...
				 * 				 
				 * Weapon w = (Weapon) Instantiate (weaponToAdd, transform.parent);
				 * w.transform.parent = transform;
				 *                 
				*/
                Destroy(col.gameObject);
			}
		}
	}
}
