using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WeaponType { Melee, Range };

public class Weapon : MonoBehaviour
{

    public WeaponType type;
    public int damage;
    public float attackRate;

    public char playerChar;

    public AudioClip weaponSound;
    public Animator weaponAnimator;

    void Update()
    {
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

    void OnCollisionEnter(Collision c)
    {
		Collider col = c.collider;
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Boom");
            PlayerManager manager = col.gameObject.GetComponent<PlayerManager>();
            char colPlayerChar = getPlayerChar();

            if (manager.getPlayerChar() != colPlayerChar)
            {
                Vector3 direction = col.transform.position - transform.position;
                manager.takeDamage(damage, direction);

				if (weaponSound != null) {
					AudioSource audioSource = GetComponent<AudioSource>();
					if (audioSource != null) {
						audioSource.clip = weaponSound;
						audioSource.Play ();
					}
				}
            }
        }
    }

    public virtual void HoldAttack(ControlButton button)
    {
    }


    public virtual void PressAttack(ControlButton button)
    {
    }

    public virtual void ReleaseAttack(ControlButton button)
    {
    }

}


