using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForestLevel : LevelManager {
	public float itemDropProb;
	public float skyBoxBlendSpeed;
	List<Vector3> originalVertices;

	Vector3 tectonicPlate;
	// Use this for initialization

	override public void Start () {
		for (int i = 0; i < 4; i++) {
			initialSpawnsList.Add (transform.GetChild (i).transform.position);
			allSpawnsList.Add (transform.GetChild (i).transform.position);
		}

		base.Start ();

	}

	// Update is called once per frame
	override public void Update () {
		base.Update ();

		if (1 - Random.Range (0.0f, 1.0f) < itemDropProb)
			spawnCrate ();
		updateSkybox ();

	}

	void spawnCrate()
	{
		Vector3 min = GetComponent<Renderer> ().bounds.min;
		Vector3 max = GetComponent<Renderer> ().bounds.max;
		Vector3 size = GetComponent<Renderer> ().bounds.size;
		Instantiate (cratePrefab, new Vector3 (Random.Range(min.x + size.x * 0.1f, max.x - size.x * 0.1f), 50, Random.Range(min.z + size.z * 0.1f, max.z - size.z * 0.1f)), Quaternion.identity);
	}


	void updateSkybox () {
		incrementSkyboxBlend (skyBoxBlendSpeed);
	}
}

