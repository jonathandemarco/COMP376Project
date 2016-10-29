using UnityEngine;
using System.Collections;

public class Fist : Weapon {
	void Start () {
		SetType (WeaponType.Melee);
		SetDamage(1);
		SetAttackRate (0.5f);
		SetAnimator ();
	}

	public override void PressAttack(ControlButton button)
    {
		YieldAttackAnimator();
	}

}
