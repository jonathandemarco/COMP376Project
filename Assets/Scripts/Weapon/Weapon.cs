using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WeaponType { Melee, Range };

public class Weapon : MonoBehaviour, MessagePassing
{

    public WeaponType type;
    public float damage;
    public float attackRate;
	public float abundance;
	public int lifetime = -1; // lifetime is probably the same as weapon durability, not sure;
	public int durability;

    public char playerChar;
	public int index;

	public AudioClip impactSound;
	public AudioClip attackSound;
    public Animator weaponAnimator;

    public virtual void Update()
    {
    }

	public virtual void playSound() {

	}

    public void setPlayerChar(char c)
    {
        playerChar = c;
    }
    public char getPlayerChar()
    {
        return playerChar;
    }

    public void SetType(WeaponType t)
    {
        type = t;
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    public void SetAttackRate(float ar)
    {
        attackRate = ar;
    }

    public float GetAttackRate()
    {
        return attackRate;
    }

    public void SetAnimator()
    {
        weaponAnimator = this.gameObject.GetComponent<Animator>();
    }

    public void SetAudioSource()
    {
        //weaponSound = this.gameObject.GetComponent<AudioSource>();
    }

    public void YieldAttackAnimator()
    {
        weaponAnimator.SetBool("isAttacking", true);
    }

    public void DisableCollider()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
    }

    public void EnableCollider()
    {
        this.gameObject.GetComponent<Collider>().enabled = true;
    }

    // changing things up so i can use it for now
    public void startAnimation()
    {
        weaponAnimator.SetBool("isAttacking", true);
    }
    public void stopAnimation()
    {
        weaponAnimator.SetBool("isAttacking", false);
    }

   	public virtual void OnCollisionEnter(Collision c)
    {
		Collider col = c.collider;

		MessagePassingHelper.passMessageOnCollision (this, col);

        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Boom");
            PlayerManager manager = col.gameObject.GetComponent<PlayerManager>();
            char colPlayerChar = getPlayerChar();
            if (manager.getPlayerChar() != colPlayerChar)
            {
				if (impactSound != null) {
					AudioSource audioSource = GetComponent<AudioSource>();
					if (audioSource != null) {
						audioSource.clip = impactSound;
						audioSource.Play ();
					}
				}
            }
        }
    }

/*	public virtual void OnTriggerEnter(Collider col){
		
		if(col.gameObject.GetComponent<PlayerManager> () != null)
			(col.gameObject.GetComponent<PlayerManager> () as MessagePassing).collisionWith (col);
		else if(col.gameObject.GetComponent<Weapon> () != null)
			(col.gameObject.GetComponent<Weapon> () as MessagePassing).collisionWith (col);
		else if(col.gameObject.GetComponent<HostileTerrain> () != null)
			(col.gameObject.GetComponent<HostileTerrain> () as MessagePassing).collisionWith (col);

		if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			Debug.Log("Boom");
			PlayerManager manager = col.gameObject.GetComponent<PlayerManager>();
			char colPlayerChar = getPlayerChar();

			if (manager.getPlayerChar() != colPlayerChar)
			{
//				Vector3 direction = col.transform.position - transform.position;
//				manager.takeDamage(damage, direction);
//				Debug.Log (direction);

				if (weaponSound != null) {
					AudioSource audioSource = GetComponent<AudioSource>();
					if (audioSource != null) {
						audioSource.clip = weaponSound;
						audioSource.Play ();
					}
				}
			}
		}
	}*/

	public virtual void HoldAttack(InputSystem button)
    {
    }


	public virtual void PressAttack(InputSystem button)
	{
	}

	public virtual void ReleaseAttack(InputSystem button)
    {
    }

	// call this method after every attack
	public void loseDurability(int d){
		durability -= d;
		Debug.Log ("Weapon broken");
		if (durability <= 0)
			GetComponentInParent<InventoryManager> ().dropWeapon (index);
	}

	void MessagePassing.collisionWith(Collider c)
	{
		
	}
}


