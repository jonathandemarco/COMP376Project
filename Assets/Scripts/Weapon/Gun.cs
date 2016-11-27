using UnityEngine;
using System.Collections;

public class Gun : Weapon {
    Renderer[] renderers;

    public Transform bulletSpawnPos;
	public GameObject mLoadingPrefab;
	public GameObject mBulletPrefab;
    GameObject loader;
	GameObject bullet;

	bool hasFired;
    bool wasloaded;
	private Vector3 goalScale;

    public void Start() {
		goalScale = transform.localScale;
		transform.Rotate (new Vector3 (-90.0f, 0.0f, 0.0f));
        renderers = this.gameObject.GetComponentsInChildren<MeshRenderer>();
		hasFired = false;
		wasloaded = false;
    }
		
	public override void Update()
	{
		base.Update ();

		if (hasFired) {
			transform.localScale /= 1.5f;

			if (transform.localScale.magnitude < 0.001f) {
				for (int i = 0; i < renderers.Length; i++) {
					renderers [i].enabled = false;
				}
				hasFired = false;
			}
		}
	}
		
	public override void PressAttack(InputSystem button) {
		transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = true;
        }
    }

	public override void ReleaseAttack (InputSystem button) 
	{
        Destroy(loader);
		bullet.SetActive (true);
		bullet.transform.parent = null;
		bullet.AddComponent<BulletMove> ();
		bullet = null;
		wasloaded = false;
		hasFired = true;
    }


	public override void HoldAttack (InputSystem button)
	{
		Vector3 diff = goalScale - transform.localScale;

		if (diff.magnitude > 0.01f)
			transform.localScale += diff / 10.0f;
		else if (!wasloaded) {
			bullet = Instantiate (mBulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation, transform) as GameObject;
			bullet.SetActive (false);
			bullet.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
			loader = Instantiate (mLoadingPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation, bulletSpawnPos) as GameObject;
			loader.transform.GetChild(0).transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
			wasloaded = true;
		}
		else if (bullet.GetComponent<Weapon> ().damage < 100) {
			bullet.GetComponent<Weapon> ().damage += 0.5f;
			loader.transform.GetChild(0).transform.localScale += new Vector3 (0.005f, 0.005f, 0.005f);
		}
	}
}
