using UnityEngine;
using System.Collections;

public enum WeaponType{Melee, Range};

public class Weapon : MonoBehaviour {

	private WeaponType type;
	private int damage;
	private float attackRate;
	Animator weaponAnimator;

	AudioSource weaponSound;

	void Start(){
		weaponAnimator = GetComponent<Animator> ();
	}

	void Update(){
		bool attack = Input.GetButtonDown("B1");
		weaponAnimator.SetBool ("isAttacking", attack);
	}

	public void SetType(WeaponType t){
		type = t;
	}

	public void SetDamage(int dmg){
		damage = dmg;
	}

	public void SetAttackRate(float ar){
		attackRate = ar;
	}

	public void SetAnimation(Animator a){
		weaponAnimator = a;
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

	public void SetAnimation(bool value){
		weaponAnimator.SetBool ("isAttacking", value); 
	}

	public virtual void HoldAttack(){}

	public virtual void PressAttack(){}

	public virtual void ReleaseAttack(){}

}
