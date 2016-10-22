using UnityEngine;
using System.Collections;

public class Gun : Weapon {
	void Start () {
		setType (WeaponType.Range);
		setDamage(5);
		setAttackRate (1.5f);
		//setAnimation(GunAnimator);
	}

	public override void pressAttack(){
		// animation.Play ();
	}

}
