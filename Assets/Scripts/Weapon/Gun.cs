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

	public override void ReleaseAttack (ControlButton button) 
	{
		Debug.Log ("Released");
		Instantiate (mBulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
	}

	public override void HoldAttack (ControlButton button) 
	{			
		Debug.Log("Charging");
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

	private void reset () {
		wasloaded = false;
		mTimer = 0.0f;
	}
}
