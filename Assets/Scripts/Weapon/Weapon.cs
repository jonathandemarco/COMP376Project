using UnityEngine;
using System.Collections;

public enum WeaponType{Melee, Range};

public class Weapon : MonoBehaviour {
	
	private Animator weaponAnimator;
	private WeaponType type;
	private int damage;
	private float attackRate;

	AudioSource weaponSound;

	public void SetType(WeaponType t){
		type = t;
	}

	public void SetDamage(int dmg){
		damage = dmg;
	}

	public void SetAttackRate(float ar){
		attackRate = ar;
	}

	public void SetWeaponActive(){
		gameObject.SetActive (true);
	}

	public void SetWeaponInactive(){
		gameObject.SetActive (false);
	}

	public void SetAnimator(){
		weaponAnimator = GetComponent<Animator> ();
	}

	public void YieldAttackAnimator(){
		bool attack = Input.GetButtonDown("B1");
		weaponAnimator.SetBool ("isAttacking", attack);
	}

	public virtual void HoldAttack(){}

	public virtual void PressAttack(){}

	public virtual void ReleaseAttack(){}

}
