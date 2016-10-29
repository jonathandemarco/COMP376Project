using UnityEngine;
using System.Collections;

public enum WeaponType{Melee, Range};

public class Weapon : MonoBehaviour {

	private WeaponType type;
	private int damage;
	private float attackRate;
    
    public char playerChar;

	private AudioSource weaponSound;
	private Animator weaponAnimator;

	void Update(){
	}

    public void setPlayerChar(char c) {
        playerChar = c;
    }
    public char getPlayerChar() {
        return playerChar;
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
			PlayerManager manager = col.gameObject.GetComponent<PlayerManager>();
			char colPlayerChar = getPlayerChar();

			if (manager.getPlayerChar () != colPlayerChar) {
				Vector3 direction = col.transform.position - transform.position;
				manager.takeDamage (damage, direction);
			}
		}
	}

	public virtual void HoldAttack (ControlButton button){
	}


	public virtual void PressAttack (ControlButton button){
	}

	public virtual void ReleaseAttack (ControlButton button){
	}

}


