using UnityEngine;
using System.Collections;

public class Bullet : Weapon {
	public float mExpirationTime;
	float mTimer = 0.0f;
	public float speed;

	public override void Start()
	{
		
	}

	public override void Update()
	{
		destroyovertime ();
		transform.position += transform.right * Time.deltaTime * -speed;
	}

	public void Setup(float damage, float maxDamage)
	{
		transform.localScale = (damage / maxDamage) * 5.0f * new Vector3(1, 1, 1);
		this.damage = damage;
	}

	void destroyovertime ()
	{
		mTimer += Time.deltaTime;
		if (mTimer >= mExpirationTime) {
			Destroy (gameObject);
		}
	}
	public void setExpirationTime(float ex) {
		mExpirationTime = ex;
	}

	public override void OnCollisionEnter(Collision c)
	{
		base.OnCollisionEnter (c);
		GetComponent<MeshRenderer>().enabled = false;
		transform.GetChild (0).gameObject.SetActive (false);

		Destroy (gameObject, 2.0f);
	}
}
	

