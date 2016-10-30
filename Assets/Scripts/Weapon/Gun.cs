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

	void ReleaseAttack (ControlButton button) 
	{
			Instantiate (mBulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
	}

	void HoldAttack (ControlButton button) 
	{			
		if (!wasloaded)
		{
			loader = (GameObject)Instantiate (mLoadingPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation, bulletSpawnPos);

			wasloaded = true;
		}

		mTimer += Time.deltaTime;
		if (mTimer > loadtime) 
		{
			Destroy (loader);
			Instantiate (mBulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
			reset ();
		} 
		else 
		{
			Destroy (loader);
			reset ();
		}			
	}

	void reset () {
		wasloaded = false;
		mTimer = 0.0f;
	}
}
