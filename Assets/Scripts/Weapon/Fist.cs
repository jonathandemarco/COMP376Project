using UnityEngine;
using System.Collections;

public class Fist : Weapon {
	void Start () {
		SetType (WeaponType.Melee);
		SetDamage(1);
		SetAttackRate (0.5f);
		//SetAnimation(FistAnimator);
	}

	public override void PressAttack(){
		// call the play animation;
	}

}
