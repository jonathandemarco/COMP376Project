using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	public GameObject textPrefab;
	public Material inventoryMaterial;
	new public Camera camera;
	// Use this for initialization
	void Start () {
		camera = GameObject.Find("HUDCam").GetComponent<Camera>();
		transform.localScale *= 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < transform.childCount; i++)
		{
			for (int j = 0; j < transform.GetChild (i).childCount; j++)
				transform.GetChild (i).GetChild (j).transform.RotateAround(transform.GetChild(i).GetChild(j).transform.position, new Vector3(0, 1, 0), 0.5f);
		}
	}

	public void setAvatar(PlayerManager player)
	{
		if (player == null)
			return;

		camera = GameObject.Find("HUDCam").GetComponent<Camera>();
		GetComponent<MeshFilter> ().mesh = player.transform.FindChild("Model").GetComponent<MeshFilter> ().mesh;
		GetComponent<Renderer> ().material = player.transform.FindChild("Model").GetComponentInChildren<Renderer> ().material;

		for (int i = 0; i < transform.childCount; i++)
			Destroy (transform.GetChild (i).gameObject);

		InventoryManager items = player.getInventory();
		Weapon[] weapons = items.getWeaponList ();
		float itemCount = weapons.Length;
		Bounds b = transform.parent.GetComponentInChildren<StatusBar> ().GetComponent<Renderer> ().bounds;
		transform.localPosition = new Vector3 (transform.localPosition.x, -b.size.y / 4, transform.localPosition.z);
		for(int i = 0; i < itemCount; i++)
		{
			GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			sphere.transform.SetParent (transform);
			sphere.GetComponent<Renderer> ().material = inventoryMaterial;
			sphere.layer = LayerMask.NameToLayer ("HUD");
			Vector3 scale = b.size * 0.5f;
			float n = Mathf.Clamp (scale.x, 0, 2.0f);

			sphere.transform.localScale = new Vector3 (n / itemCount, n / itemCount, n / itemCount) / 1.5f;
			float offsetY = (sphere.GetComponent<Renderer> ().bounds.size.y + transform.parent.transform.FindChild("HealthBar").GetComponent<Renderer> ().bounds.size.y) / 8;
			sphere.transform.localPosition = - new Vector3(- i * n * 2 / itemCount - 0.5f - n * 0.5f / itemCount, offsetY, 4);

			if (weapons[i] != null && !(weapons [i] is NullWeapon)) {
				GameObject w = Instantiate (weapons [i].gameObject, sphere.transform.position - new Vector3 (0, 0, 1), Quaternion.identity, sphere.transform) as GameObject;
				w.layer = LayerMask.NameToLayer ("HUD");
				w.transform.localScale = new Vector3 (1, 1, 1);
				Component[] comp = w.GetComponents<Component> ();

				foreach (Component c in comp) {
					if (!(c is Transform) && !(c is Renderer) && !(c is MeshFilter))
						Destroy (c);
				}

				comp = w.GetComponentsInChildren<Component> ();

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
				w.transform.localScale = new Vector3((float)Screen.width / 1000, (float)Screen.height / 1000, 1.5f) / maxSize;
				Vector3 diff = 1.5f * (maxBounds + minBounds) / (2 * maxSize);
				w.transform.localPosition = - new Vector3 (diff.x, diff.y, 5);

				if (w.GetComponent<Weapon> ().durability > 0) {
					GameObject durability = Instantiate (textPrefab, new Vector3 (w.transform.position.x, sphere.GetComponent<Renderer> ().bounds.min.y, w.transform.position.z), Quaternion.identity, transform) as GameObject;
					durability.layer = LayerMask.NameToLayer ("HUD");
					durability.transform.localScale = new Vector3 (0.2f * (float)Screen.width / 1000, 0.3f * (float)Screen.height / 1000, 1.0f);
					TextMesh durabilityText = durability.GetComponent<TextMesh> ();
					durabilityText.text = "" + weapons [i].durability;
				}
			}
		}

		if (player.numLives > 0) {
			Vector3 ballSize = GetComponent<Renderer> ().bounds.size;

			GameObject numOfLives = Instantiate (textPrefab, transform.position + new Vector3 (ballSize.x * 0.8f, ballSize.y * 0.8f, 0), Quaternion.identity, transform) as GameObject;
			numOfLives.layer = LayerMask.NameToLayer ("HUD");
			numOfLives.transform.localScale = new Vector3 (0.1f * (float)Screen.width / 1000, 0.4f * (float)Screen.height / 1000, 1.0f);
			TextMesh text = numOfLives.GetComponent<TextMesh> ();
			text.text = "x";

			Vector3 xSize = numOfLives.GetComponent<Renderer> ().bounds.size;
			numOfLives = Instantiate (textPrefab, transform.position, Quaternion.identity, transform) as GameObject;
			numOfLives.layer = LayerMask.NameToLayer ("HUD");
			numOfLives.transform.localScale = new Vector3 (0.4f * (float)Screen.width / 1000, 0.7f * (float)Screen.height / 1000, 1.0f);
			numOfLives.transform.localPosition = new Vector3 (ballSize.x * 0.6f + xSize.x / 2 + numOfLives.GetComponent<Renderer> ().bounds.size.x / 2, ballSize.y * 0.6f, 0);
			text = numOfLives.GetComponent<TextMesh> ();
			text.text = "" + player.getNumLives ();
		}
	}
}
