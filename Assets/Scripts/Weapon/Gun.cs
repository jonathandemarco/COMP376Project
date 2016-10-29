using UnityEngine;
using System.Collections;

public class Gun : Weapon {

	public Transform bulletSpawnPos;
	public GameObject mLoadingPrefab;
	public GameObject mBulletPrefab;
	GameObject loader;
	bool wasloaded;
	float loadtime = 3.0f;
	float mTimer = 0.0f;

	void Start () {
		SetType (WeaponType.Range);
		SetDamage(5);
		SetAttackRate (1.5f);
		SetAnimator ();
	}

	public override void PressAttack(){
		YieldAttackAnimator();
	}

	void Update () {
		//spawnBullet ();
		loadFire ();
	}

//	void spawnBullet () {
//
//		if (Input.GetKeyDown (KeyCode.Space))
//			Instantiate (mBulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
//	}

	void loadFire () {
		
		if (Input.GetKey (KeyCode.Space)) {
			
			if (!wasloaded) {
				loader = (GameObject)Instantiate (mLoadingPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation, bulletSpawnPos);
				wasloaded = true;
			}

			mTimer += Time.deltaTime;
			if (mTimer > loadtime) {
				Destroy (loader);
				Instantiate (mBulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
				reset ();
			}

		} else {
			Destroy (loader);
			reset ();
		}
			
	}

	void reset () {
		wasloaded = false;
		mTimer = 0.0f;
	}
}
