using UnityEngine;
using System.Collections;

public class Gun : Weapon {
	void Start () {
		SetType (WeaponType.Range);
		SetDamage(5);
		SetAttackRate (1.5f);
		//setAnimation(GunAnimator);
	}

	public override void PressAttack(){
		// call the play animation;
	}

}
