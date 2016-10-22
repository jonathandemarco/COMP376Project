using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed = 5.0f;
	
	void Update(){
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		Vector3 direction = new Vector3 (horizontal, 0, vertical);
		transform.position += (direction) * moveSpeed * Time.deltaTime;



	}
		

}
