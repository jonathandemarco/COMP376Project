using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WeaponType { Melee, Range };

public class Weapon : MonoBehaviour, MessagePassing
{
	public Renderer[] renderers;
    public WeaponType type;
    public float damage;
    public float attackRate;
	public int durability;
	public Vector3 goalScale;

    public char playerChar;
	public int index;

	public AudioClip impactSound;
	public AudioClip attackSound;
    public Animator weaponAnimator;

	public bool onDisplayLoop;
	public bool onHideLoop;

	public virtual void Start()
	{
		onDisplayLoop = false;
		onHideLoop = false;
		goalScale = transform.localScale;
		List<Renderer> tempRend = new List<Renderer> ();

		renderers = GetComponentsInChildren<Renderer>();

		if(GetComponent<Renderer> ())
			tempRend.Add (GetComponent<Renderer> ());

		if(renderers != null && renderers.Length > 0)
			for (int i = 0; i < renderers.Length; i++) {
				tempRend.Add (renderers [i]);
			}
				
		renderers = tempRend.ToArray ();

		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = false;
		}
	}

    public virtual void Update()
    {
		if (onDisplayLoop) {
			Vector3 diff = goalScale - transform.localScale;

			if (Mathf.Abs (1.0f - transform.localScale.magnitude / goalScale.magnitude) > 0.01f) {
				transform.localScale += diff / 10.0f;
			}
			else
				onDisplayLoop = false;
		}
		else if(onHideLoop)
		{
			transform.localScale /= 1.5f;

			if (transform.localScale.magnitude < 0.001f) {
				for (int i = 0; i < renderers.Length; i++) {
					renderers [i].enabled = false;
				}
				onHideLoop = false;
			}
		}
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

	public virtual void display()
	{
		transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);

		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = true;
		}

		onDisplayLoop = true;
		onHideLoop = false;
	}

	public virtual void hide()
	{
		onHideLoop = true;
		onDisplayLoop = false;
	}

	void MessagePassing.collisionWith(Collider c)
	{
		
	}
}


