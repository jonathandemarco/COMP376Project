using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setAvatar(PlayerManager player)
	{
		GetComponent<MeshFilter> ().mesh = player.GetComponent<MeshFilter> ().mesh;
		GetComponent<Renderer> ().material = player.GetComponent<Renderer> ().material;

		InventoryManager items = player.getInventory();
		Weapon[] weapons = items.getWeaponList ();
		float itemCount = weapons.Length;
		Bounds b = transform.parent.GetComponentInChildren<StatusBar> ().GetComponent<Renderer> ().bounds;
		for(int i = 0; i < itemCount; i++)
		{
			GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			sphere.transform.SetParent (transform);
			sphere.layer = LayerMask.NameToLayer ("HUD");
			Vector3 scale = b.size * 0.5f;
			float n = Mathf.Clamp (scale.x, 0, 2.0f);
			sphere.transform.position = transform.position - new Vector3(- i * n * 2 / itemCount - 2 - n * 0.5f / itemCount, 2, 0);
			sphere.transform.localScale = new Vector3 (n / itemCount, n / itemCount, 0.2f);
			Instantiate (weapons [i].gameObject);
		}
	}
}
