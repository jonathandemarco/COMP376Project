using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DesertLevel : LevelManager {
	public float meteorProb;
	public GameObject meteorShowerPrefab;
	List<Vector3> originalVertices;
	float animationTime;
	private GravitySource gSource;
	bool deformation = false;

	// Use this for initialization
	override public void Start () {
		base.Start ();
		animationTime = 0;
		//		setUpMesh (50);

		RenderSettings.skybox = skyboxMat;
	}

	// Update is called once per frame
	override public void Update () {
		base.Update ();

		if (1 - Random.Range (0.0f, 1.0f) < meteorProb)
			spawnMeteorShower ();

		if (1 - Random.Range (0.0f, 1.0f) < itemDropProb)
			spawnCrate ();

		updateSkybox ();
	}
		
	void spawnCrate()
	{
		Vector3 min = GetComponent<Renderer> ().bounds.min;
		Vector3 max = GetComponent<Renderer> ().bounds.max;
		Vector3 size = GetComponent<Renderer> ().bounds.size;
		Instantiate (cratePrefab, new Vector3 (Random.Range(min.x + size.x * 0.1f, max.x - size.x * 0.1f), 20, Random.Range(min.z + size.z * 0.1f, max.z - size.z * 0.1f)), Quaternion.identity);
	}

	void spawnMeteorShower() {
		Vector3 min = GetComponent<Renderer> ().bounds.min;
		Vector3 max = GetComponent<Renderer> ().bounds.max;
		Vector3 size = GetComponent<Renderer> ().bounds.size;
		MeteorShower script = (MeteorShower) Instantiate (meteorShowerPrefab).GetComponent(typeof (MeteorShower));
		script.setCastPoint (new Vector3 (Random.Range (min.x + size.x * 0.1f, max.x - size.x * 0.1f), 0, Random.Range (min.z + size.z * 0.1f, max.z - size.z * 0.1f)));
	}

	void updateSkybox () {
		incrementSkyboxBlend (skyBoxBlendSpeed);
	}
}
