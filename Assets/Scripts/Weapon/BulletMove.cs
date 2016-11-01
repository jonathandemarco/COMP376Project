using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour
{

	float mExpirationTime = 5.0f;
	float mTimer = 0.0f;
	float speed = 20.0f;

	// Use this for initialization
	void Start ()
	{
	
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




