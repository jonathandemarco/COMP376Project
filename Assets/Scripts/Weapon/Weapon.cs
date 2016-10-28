using UnityEngine;
using System.Collections;

public enum WeaponType{Melee, Range};

public class Weapon : MonoBehaviour {
	
	private WeaponType type;
	public int damage;
	public float attackRate;

    public char playerChar;

	private AudioSource weaponSound;
	private Animator weaponAnimator;

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
		weaponAnimator = GetComponent<Animator> ();
	}

	public void SetAudioSource(){
		weaponSound = GetComponent<AudioSource> ();
	}

	public void YieldAttackAnimator(){
		weaponAnimator.SetBool ("isAttacking", true);
	}
    // changing things up so i can use it for now
    public void startAnimation() {
        weaponAnimator.SetBool("isAttacking", true);
    }
    public void stopAnimation() {
        weaponAnimator.SetBool("isAttacking", false);
    }
     
    public void SetColliderActive(){
		GetComponent<Collider> ().enabled = true;
	}

	public void SetColliderInactive(){
		GetComponent<Collider> ().enabled = false;
	}

	public virtual void HoldAttack(ControlButton button){}

	public virtual void PressAttack(ControlButton button){ }

	public virtual void ReleaseAttack(ControlButton button) {}

}


