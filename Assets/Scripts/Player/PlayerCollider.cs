using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour
{

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GetComponentInChildren<InventoryManager>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Crate"))
        {
            Debug.Log("Picked up weapon!");
            if (inventoryManager.GetWeaponCount() < inventoryManager.maxInventorySize)
            {
                //add the item to player's inventory
                int id = col.GetComponent<Crate>().IDValue;
                WeaponDatabase database = inventoryManager.GetWeaponDatabase();
                Destroy(col.gameObject);

                Weapon weaponToAdd = database.GetComponent<WeaponDatabase>().GetWeaponAt(id);

                inventoryManager.AddToInventory(weaponToAdd);

                notify();

            }
        }

        /*
		 * The following code is now located in the Weapon script
		 * 
        if (col.gameObject.layer == LayerMask.NameToLayer("Weapon")) {
            PlayerManager manager = GetComponent<PlayerManager>();
            char colPlayerChar = col.GetComponent<Weapon>().getPlayerChar();
            if (manager.getPlayerChar() != colPlayerChar ){
                Vector3 direction = transform.position - col.transform.position;
                manager.takeDamage(col.GetComponent<Weapon>().damage, direction);
            }
        }
		*/

    }

    // Notifies all pertinent observers of any changes made to the player
    private void notify()
    {
        GetComponent<PlayerManager>().notify();
    }
}
