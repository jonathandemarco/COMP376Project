using UnityEngine;
using System.Collections;

public class FlameThrower : Weapon {
    Renderer[] renderers;

    public Transform bulletSpawnPos;
	public GameObject flame;
    GameObject loader;
    public float fireRate;
	private float fireTimer = 0;


    public void Start() {


    }
	public override void PressAttack(InputSystem button) {


    }
	public override void ReleaseAttack (InputSystem button) 
	{

    }


	public override void HoldAttack (InputSystem button)
	{
		fireTimer += Time.deltaTime;
		if (fireTimer < fireRate) {
			Instantiate (flame, bulletSpawnPos.position, transform.parent.rotation);
			fireTimer = 0;
		}
 
	}
    private void fire() {


    }
}
