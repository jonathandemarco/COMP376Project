using UnityEngine;
using System.Collections;

public enum WeaponType{Melee, Range};

public class Weapon : MonoBehaviour {
	
	private WeaponType type;
	private int damage;
	private float attackRate;

	private AudioSource weaponSound;
	private Animator weaponAnimator;

	void Update(){
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

	public void SetAnimator(){
		weaponAnimator = this.gameObject.GetComponent<Animator>();
	}

	public void SetAudioSource(){
		weaponSound = this.gameObject.GetComponent<AudioSource> ();
	}

	public void YieldAttackAnimator(){
		weaponAnimator.SetBool ("isAttacking", true);
	}

	public void DisableCollider(){
		this.gameObject.GetComponent<Collider> ().enabled = false;
	}

	public void EnableCollider(){
		this.gameObject.GetComponent<Collider> ().enabled = true;
	}

    // changing things up so i can use it for now
    public void startAnimation() {
        weaponAnimator.SetBool("isAttacking", true);
    }
    public void stopAnimation() {
        weaponAnimator.SetBool("isAttacking", false);
    }

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
			// for now, im checking if the collider is the player then I call the take damage function
			// We'll use another way to perform this action since weapon can make the player do more things than just take damage
			col.gameObject.GetComponent <PlayerManager>().takeDamage(damage);
		}
	}

	public virtual void HoldAttack (ControlButton button){
	}


	public virtual void PressAttack (ControlButton button){
	}

	public virtual void ReleaseAttack (ControlButton button){
	}

}


