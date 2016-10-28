using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

    private InventoryManager inventoryManager;

    void Awake() {
       inventoryManager =  GetComponent<PlayerManager>().getInventory();
    }
    void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "Crate") {
            Debug.Log("Picked up weapon!");
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

        if (col.gameObject.layer == LayerMask.NameToLayer("Weapon")) {
            PlayerManager manager = transform.parent.GetComponent<PlayerManager>();
            PlayerManager colManager = col.transform.parent.GetComponent<PlayerManager>();
            if (manager.getPlayerChar() != colManager.getPlayerChar()) {
                manager.takeDamage(col.GetComponent<Weapon>().damage);
            }
        }


    }
}
