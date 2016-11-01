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
		GetComponent<MeshFilter> ().mesh = player.transform.FindChild("Model").GetComponent<MeshFilter> ().mesh;
		GetComponent<Renderer> ().material = player.transform.FindChild("Model").GetComponentInChildren<Renderer> ().material;

		for (int i = 0; i < transform.childCount; i++)
			Destroy (transform.GetChild (i).gameObject);

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
			sphere.transform.position = transform.position - new Vector3(- i * n * 2 / itemCount - 2 - n * 0.5f / itemCount, 2, 2);
			sphere.transform.localScale = new Vector3 (n / itemCount, n / itemCount, n / itemCount);
			if (!(weapons [i] is NullWeapon)) {
				GameObject w = Instantiate (weapons [i].gameObject, sphere.transform.position - new Vector3 (0, 0, 1), Quaternion.identity, sphere.transform) as GameObject;
				w.layer = LayerMask.NameToLayer ("HUD");
				w.transform.localScale = new Vector3 (1, 1, 1);
				Component[] comp = w.GetComponents<Component> ();

				foreach (Component c in comp) {
					if (!(c is Transform) && !(c is Renderer) && !(c is MeshFilter))
						Destroy (c);
				}
			
				Renderer[] renderers = w.GetComponentsInChildren<Renderer> ();

				for (int j = 0; j < renderers.Length; j++) {
					renderers [j].enabled = true;
					renderers [j].gameObject.layer = LayerMask.NameToLayer ("HUD");
				}

				Bounds bounds = new Bounds();
				Vector3 minBounds = new Vector3 (Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
				Vector3 maxBounds = new Vector3 (-Mathf.Infinity, -Mathf.Infinity, -Mathf.Infinity);

				MeshFilter[] meshFilters = w.GetComponentsInChildren<MeshFilter> ();

				for(int j = 0; j < meshFilters.Length; j++)
					bounds.Encapsulate (meshFilters [j].mesh.bounds);
				
				minBounds = bounds.min;
				maxBounds = bounds.max;
				Vector3 size = maxBounds - minBounds;
				float maxSize = Mathf.Max (size.x, Mathf.Max(size.y, size.z));
				w.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f) / maxSize;
				Vector3 diff = 1.5f * (maxBounds + minBounds) / (2 * maxSize);
				w.transform.localPosition = - new Vector3 (diff.x, diff.y, 5);
			}
		}
	}
}
