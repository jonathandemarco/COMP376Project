using UnityEngine;
using System.Collections;

public class FlameThrower : Weapon {
    Renderer[] renderers;

    public Transform bulletSpawnPos;
	public GameObject mFirerfab;
    GameObject loader;
    public float flameDistance;
    public float fireRate;
    private float lastFire = 0;

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
    }


	public override void HoldAttack (ControlButton button)
	{
        Debug.Log ("Charging");
        if (Time.time > lastFire) {
            fire();
            lastFire = Time.time + fireRate;
        }
	}
    private void fire() {


    }
}
