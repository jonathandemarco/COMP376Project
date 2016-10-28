using UnityEngine;
using System.Collections;

public class Gun : Weapon {
	void Start () {
		SetType (WeaponType.Range);
		SetDamage(5);
		SetAttackRate (1.5f);
		SetAnimator ();
	}

	public override void PressAttack(ControlButton button)
    {
		YieldAttackAnimator();
	}

}
