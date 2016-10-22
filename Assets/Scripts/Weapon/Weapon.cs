using UnityEngine;
using System.Collections;

public enum WeaponType{Melee, Range};

public class Weapon : MonoBehaviour {

	private WeaponType type;
	private int damage;
	private float attackRate;
	Animator weaponAnimator;

	AudioSource weaponSound;

	public void StartAttack(){
		// Play the animation
		// animation.Play ();
	}

	public void setType(WeaponType t){
		type = t;
	}

	public void setDamage(int dmg){
		damage = dmg;
	}

	public void setAttackRate(float ar){
		attackRate = ar;
	}

	public void setAnimation(Animator a){
		weaponAnimator = a;
	}

	public virtual void holdAttack(){}

	public virtual void pressAttack(){}

	public virtual void releaseAttack(){
		// stop the animation
		// animation.Stop();
	}

}
