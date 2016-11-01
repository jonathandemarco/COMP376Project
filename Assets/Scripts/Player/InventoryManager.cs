﻿using UnityEngine;
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
        for (int i = 0; i < maxInventorySize; i++)
        {
            inventory[i] = nullWeapon;
        }
    }

    public void AddToInventory(Weapon wep)
    {
        Weapon w = (Weapon)Instantiate(wep, new Vector3(0, 0, 0), Quaternion.LookRotation(Vector3.up, Vector3.forward));
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
}
