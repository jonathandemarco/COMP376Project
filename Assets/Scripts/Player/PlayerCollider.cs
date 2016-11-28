using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour, MessagePassing
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
			}
			//Destroy (col.gameObject);
			notify ();
		}
		
	}
	
	void MessagePassing.collisionWith(Collider c)
	{
		Vector3 direction = transform.position - c.transform.position;
		if (c.gameObject.layer == LayerMask.NameToLayer ("Weapon")) {
			if (c.gameObject.GetComponent<Weapon> ().getPlayerChar () != GetComponent<PlayerManager> ().getPlayerChar ()) {
                GameObject owner = c.gameObject.GetComponent<Weapon>().getPlayerOwner();
                GetComponent<PlayerManager> ().takeDamage (c.gameObject.GetComponent<Weapon> ().damage, direction, owner);
			}
		} else if (c.gameObject.layer == LayerMask.NameToLayer ("HostileTerrain")) {
			GetComponent<PlayerManager> ().takeDamage (c.gameObject.GetComponent<HostileTerrain> ().damage, direction, null);
		} else if (c.gameObject.layer == LayerMask.NameToLayer ("Crate")) {
			if (inventoryManager.GetWeaponCount () < inventoryManager.maxInventorySize) {
				//add the item to player's inventory
				int id = c.GetComponent<Crate> ().IDValue;
				WeaponDatabase database = inventoryManager.GetWeaponDatabase ();
				Weapon weaponToAdd = database.GetComponent<WeaponDatabase> ().GetWeaponAt (id);
				inventoryManager.AddToInventory (weaponToAdd);
			}
			/* 
				Weapon w = (Weapon) Instantiate (weaponToAdd, transform.parent);
                w.setPlayerChar(GetComponent<PlayerManager>().getPlayerChar());
                w.transform.parent = transform; */
			notify ();
		}
	}
	
    // Notifies all pertinent observers of any changes made to the player
    private void notify()
    {
        GetComponent<PlayerManager>().notify();
    }
}
