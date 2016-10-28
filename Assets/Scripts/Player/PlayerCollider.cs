using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

    private InventoryManager inventoryManager;

    void Start() {
        inventoryManager = GetComponentInChildren<InventoryManager>();
    }
    void OnTriggerEnter(Collider col){
        Debug.Log("Woop!");
		if (col.gameObject.name == "Crate") {
            Debug.Log("Picked up weapon!");
			if (inventoryManager.GetWeaponCount() < inventoryManager.maxInventorySize) {
				//add the item to player's inventory
				int id = col.GetComponent<Crate> ().IDValue;
				WeaponDatabase database = inventoryManager.GetWeaponDatabase ();
				Weapon weaponToAdd = database.GetComponent<WeaponDatabase> ().GetWeaponAt (id); 
				inventoryManager.AddToInventory (weaponToAdd);
				Weapon w = (Weapon) Instantiate (weaponToAdd, transform.parent);
                w.setPlayerChar(GetComponent<PlayerManager>().getPlayerChar());
                w.transform.parent = transform;


                Destroy(col.gameObject);
			}
		}

        if (col.gameObject.layer == LayerMask.NameToLayer("Weapon")) {
            PlayerManager manager = GetComponent<PlayerManager>();
            char colPlayerChar = col.GetComponent<Weapon>().getPlayerChar();
            if (manager.getPlayerChar() != colPlayerChar ){
                Vector3 direction = transform.position - col.transform.position;
                manager.takeDamage(col.GetComponent<Weapon>().damage, direction);
            }
        }


    }
}
