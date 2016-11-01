using UnityEngine;
using System.Collections;

public class Gun : Weapon {
    Renderer[] renderers;

    public Transform bulletSpawnPos;
	public GameObject mLoadingPrefab;
	public GameObject mBulletPrefab;
    GameObject loader;

    bool wasloaded;
	float loadtime = 3.0f;
	float mTimer = 0.0f;
    private bool hasFired = false;

    public void Start() {

        renderers = this.gameObject.GetComponentsInChildren<MeshRenderer>();

    }
    public override void PressAttack(ControlButton button) {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = true;
        }

    }
    public override void ReleaseAttack (ControlButton button) 
	{
		Debug.Log ("Released");

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }
        Destroy(loader);
        reset();
    }


	public override void HoldAttack (ControlButton button)
	{
        // if(button.allowAttack()){
        Debug.Log ("Charging");
			if (!wasloaded) {
				loader = (GameObject)Instantiate (mLoadingPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation, bulletSpawnPos);
				wasloaded = true;
			}

			if (button.getHoldTime() > loadtime && !hasFired) {
				Destroy (loader);
				Instantiate (mBulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
                hasFired = true;
			} 

	  //}
	}

	private void reset () {
		wasloaded = false;
        hasFired = false;
	}
}
