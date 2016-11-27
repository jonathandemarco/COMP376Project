using UnityEngine;
using System.Collections;

public class NullWeapon : Weapon
{
	public override void PressAttack(InputSystem button)
    {
        Debug.Log("NullWeapon");
    }
	public override void HoldAttack(InputSystem button)
    {
        Debug.Log("NullWeapon");
    }
	public override void ReleaseAttack(InputSystem button)
    {
        Debug.Log("NullWeapon");
    }
}