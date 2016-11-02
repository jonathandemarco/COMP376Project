using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class InventoryManager : MonoBehaviour
{

    public Weapon[] inventory;
    public int maxInventorySize;
    private WeaponDatabase database;
    public Weapon nullWeapon;


    void Start()
    {
        inventory = new Weapon[maxInventorySize];
        database = GameObject.FindGameObjectWithTag("Weapon Database").GetComponent<WeaponDatabase>();
        nullWeapon = database.getNullWeapon();
        
        resetInventory(true);
    }

    public void AddToInventory(Weapon wep)
    {
		// check if weapon is already in inventory
		for (int i = 0; i < inventory.Length; i++) {
			Debug.Log (inventory [i].name);
			Debug.Log (wep.name);
			if (string.Equals(wep.name, inventory [i].name.Substring(0, inventory [i].name.Length-7))) {
				Debug.Log ("Player has this weapon already");
				return;
			}
		}

		Weapon w = (Weapon)Instantiate(wep, new Vector3(0, 0, 0), Quaternion.LookRotation(gameObject.transform.right, gameObject.transform.up));
        w.setPlayerChar(GetComponentInParent<PlayerManager>().getPlayerChar());
		w.transform.position = transform.position;
        w.transform.parent = transform;

        Renderer[] renderers = w.gameObject.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == nullWeapon)
            {
                inventory[i] = w;
                break;
            }
        }
    }
    public Weapon GetWeapon(int i)
    {
        if (i > GetWeaponCount())
        {
            return nullWeapon;
        }
        return inventory[i];
    }

    public Weapon[] getWeaponList()
    {
        return inventory;
    }
    public void DropFromInventory(int index)
    {
        inventory[index] = nullWeapon;
        //TODO Drop?
    }

    public int GetWeaponCount()
    {
        int count = 0;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != nullWeapon)
                count++;
        }
        return count;
    }

    public WeaponDatabase GetWeaponDatabase()
    {
        return database;
    }
    public void dropWeapon(int index) {
        if (index < maxInventorySize && inventory[index]!=nullWeapon)
        {

            Weapon w = inventory[index];
            inventory[index] = nullWeapon;
            w.gameObject.SetActive(false);

        }
    }
    public void resetInventory(bool start) {

        for (int i = 0; i < maxInventorySize; i++) {
            if(start)
                inventory[i] = nullWeapon;
            else
                dropWeapon(i);
        }
        Weapon w = (Weapon)Instantiate(database.GetWeaponAt(0), new Vector3(0, 0, 0), Quaternion.LookRotation(gameObject.transform.right, gameObject.transform.up));
        w.setPlayerChar(GetComponentInParent<PlayerManager>().getPlayerChar());
        w.transform.position = transform.position;
        w.transform.parent = transform;
        inventory[0] = w;
        Renderer[] renderers = w.gameObject.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }
        
    }
}
