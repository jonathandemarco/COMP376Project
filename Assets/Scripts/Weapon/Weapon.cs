using UnityEngine;
using System.Collections;

public enum WeaponType{Melee, Range};

public class Weapon : MonoBehaviour {
	
	private WeaponType type;
	private int damage;
	private float attackRate;

	private AudioSource weaponSound;
	private Animator weaponAnimator;

	public void SetType(WeaponType t){
		type = t;
	}

	public void SetDamage(int dmg){
		damage = dmg;
	}

	public void SetAttackRate(float ar){
		attackRate = ar;
	}

	public void SetAnimator(){
		weaponAnimator = GetComponent<Animator> ();
	}

	public void SetAudioSource(){
		weaponSound = GetComponent<AudioSource> ();
	}

	public void YieldAttackAnimator(){
		bool attack = Input.GetButtonDown("B1");
		weaponAnimator.SetBool ("isAttacking", attack);
	}

	public void SetColliderActive(){
		GetComponent<Collider> ().enabled = true;
	}

	public void SetColliderInactive(){
		GetComponent<Collider> ().enabled = false;
	}

	public virtual void HoldAttack(){}

	public virtual void PressAttack(){}

	public virtual void ReleaseAttack(){}

}
