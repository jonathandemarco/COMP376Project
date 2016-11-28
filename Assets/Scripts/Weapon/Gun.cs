using UnityEngine;
using System.Collections;

public class Gun : Weapon {
    public Transform bulletSpawnPos;
	public GameObject mLoadingPrefab;
	public GameObject mBulletPrefab;
	public float maxDamage;
	public float chargingRate;
    GameObject loader;
	GameObject bullet;

    bool wasloaded;

    public override void Start() {
		base.Start ();
		transform.Rotate (new Vector3 (-90.0f, 0.0f, 0.0f));
		wasloaded = false;
		damage = 0;
    }
		
	public override void PressAttack(InputSystem button) {
		display ();
		wasloaded = false;
		damage = 0;
    }

	public override void ReleaseAttack (InputSystem button) 
	{
		if (loader != null) {
			Destroy (loader);
			loader = null;
		}

		if (damage > 0) {
			bullet = Instantiate (mBulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation) as GameObject;
			bullet.GetComponent<Bullet> ().Setup (damage, maxDamage);
			bullet = null;
			loseDurability ((int)((damage / maxDamage) * 5.0f));
		}

		wasloaded = false;
		hide ();
		damage = 0;
    }

	public override void HoldAttack (InputSystem button)
	{
		if ((1.0f - transform.localScale.magnitude / goalScale.magnitude) < 0.01f) {
			if (!wasloaded) {
				if(loader == null)
					loader = Instantiate (mLoadingPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation, bulletSpawnPos) as GameObject;
				wasloaded = true;
			} else if (damage < maxDamage) {
				damage += chargingRate * Time.deltaTime;
				loader.transform.localScale += new Vector3 (10.0f, 10.0f, 10.0f) * Time.deltaTime;
			}
		}
	}
}
