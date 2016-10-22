using UnityEngine;
using System.Collections;

public class Fist : Weapon {
	void Start () {
		setType (WeaponType.Melee);
		setDamage(1);
		setAttackRate (0.5f);
		//setAnimation(FistAnimator)
	}

	public override void pressAttack(){
		// animation.Play ();
	}

}
