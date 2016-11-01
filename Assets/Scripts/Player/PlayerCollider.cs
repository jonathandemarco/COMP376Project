using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour
{

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GetComponentInChildren<InventoryManager>();
    }

/*    void OnCollisionrEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Crate"))
        {
            Debug.Log("Picked up weapon!");
			if (inventoryManager.GetWeaponCount() < inventoryManager.maxInventorySize) {
				//add the item to player's inventory
				int id = col.GetComponent<Crate> ().IDValue;
				WeaponDatabase database = inventoryManager.GetWeaponDatabase ();
				Weapon weaponToAdd = database.GetComponent<WeaponDatabase> ().GetWeaponAt (id);
				inventoryManager.AddToInventory (weaponToAdd);

				/* 
				Weapon w = (Weapon) Instantiate (weaponToAdd, transform.parent);
                w.setPlayerChar(GetComponent<PlayerManager>().getPlayerChar());
                w.transform.parent = transform;
				*/
 /*               Destroy(col.gameObject);
			}
			notify ();
		}	
    }*/

	void OnCollisionEnter(Collision c)
	{
		Collider col = c.collider;
		if (col.gameObject.layer == LayerMask.NameToLayer ("Crate")) {
			Debug.Log ("Picked up weapon!");
			if (inventoryManager.GetWeaponCount () < inventoryManager.maxInventorySize) {
				//add the item to player's inventory
				int id = col.GetComponent<Crate> ().IDValue;
				WeaponDatabase database = inventoryManager.GetWeaponDatabase ();
				Weapon weaponToAdd = database.GetComponent<WeaponDatabase> ().GetWeaponAt (id);
				inventoryManager.AddToInventory (weaponToAdd);

				/* 
				Weapon w = (Weapon) Instantiate (weaponToAdd, transform.parent);
                w.setPlayerChar(GetComponent<PlayerManager>().getPlayerChar());
                w.transform.parent = transform;
				*/
				Destroy (col.gameObject);
			}
			else {
				Destroy (col.gameObject);
			}
			notify ();
		}
		
	}

    // Notifies all pertinent observers of any changes made to the player
    private void notify()
    {
        GetComponent<PlayerManager>().notify();
    }
}
