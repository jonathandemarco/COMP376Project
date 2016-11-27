using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour
{

	public float mExpirationTime;
	float mTimer = 0.0f;
	ParticleSystem.Particle[] m_Particles;
	public float speed;

	// Use this for initialization
	void Start ()
	{
		transform.localScale = (GetComponent<Weapon> ().damage / 100.0f) * 5.0f * new Vector3(1.0f, 1.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		destroyovertime ();
		transform.position += transform.right * Time.deltaTime * -speed;
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
		
}




