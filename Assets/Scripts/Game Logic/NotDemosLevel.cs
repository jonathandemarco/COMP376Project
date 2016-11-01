using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotDemosLevel : LevelManager {
	public float itemDropProb;
	public float meteorProb;
	public float deformationProb;
	public GameObject meteorShowerPrefab;
	public float skyBoxBlendSpeed;
	List<Vector3> originalVertices;
	float animationTime;
	public float warningTime;
	public float warningStep;
	private float earthquakeAnimTime;
	bool deformation = false;
	Vector3 tectonicPlate;
	// Use this for initialization

	override public void Start () {
		for (int i = 0; i < 4; i++) {
			initialSpawnsList.Add (transform.GetChild (i).transform.position);
			allSpawnsList.Add (transform.GetChild (i).transform.position);
		}

		base.Start ();

		animationTime = 0;
		GetComponent<MeshFilter> ().mesh = new Mesh ();

		originalVertices = new List<Vector3> ();
		List<Vector3> vertices = new List<Vector3> ();
		List<Vector2> uv = new List<Vector2> ();
		List<int> triangles = new List<int> ();

		int resolution = 50;

		for(int j = 0; j <= resolution; j++)
		{
			for (int i = 0; i <= resolution; i++) {
				vertices.Add (new Vector3 (((float)i / (float)resolution) - 0.5f, ((float)j / (float)resolution) - 0.5f, 0.0f));
				uv.Add (new Vector2((float)i / (float)resolution, (float)j / (float)resolution));
				originalVertices.Add (vertices[vertices.Count - 1]);
				if (i > 0 && j > 0) {
					triangles.Add (vertices.Count - 2);
					triangles.Add (vertices.Count - 1);
					triangles.Add (vertices.Count - resolution - 2);

					triangles.Add (vertices.Count - 2);
					triangles.Add (vertices.Count - resolution - 2);
					triangles.Add (vertices.Count - resolution - 3);
				}
			}
		}
		GetComponent<MeshFilter> ().mesh.vertices = vertices.ToArray();
		GetComponent<MeshFilter> ().mesh.uv = uv.ToArray();
		GetComponent<MeshFilter> ().mesh.triangles = triangles.ToArray();
		GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
		GetComponent<MeshFilter> ().mesh.RecalculateNormals ();

		GetComponent<MeshCollider> ().sharedMesh = GetComponent<MeshFilter> ().mesh;

		RenderSettings.skybox = skyboxMat;
	}
	
	// Update is called once per frame
	override public void Update () {
		base.Update ();

		if (1 - Random.Range (0.0f, 1.0f) < meteorProb)
			spawnMeteorShower ();

		if (1 - Random.Range (0.0f, 1.0f) < itemDropProb)
			spawnCrate ();

		if (1 - Random.Range (0.0f, 1.0f) < deformationProb)
			deformation = true;

		updateSkybox ();

		if (animationTime == 0.0f && deformation) {
			animationTime += Time.deltaTime;
			tectonicPlate = new Vector3 (Random.Range (-0.5f, 0.5f), Random.Range (-0.5f, 0.5f), 0);
		}

		if(animationTime > 0.0f && animationTime < warningTime)
		{
			transform.position = new Vector3(transform.position.x, -Mathf.Sin(animationTime / 10), transform.position.z);
			if(earthquakeAnimTime > warningStep)
			{
				earthquakeAnimTime = 0;

				Vector3[] vertices = new Vector3[originalVertices.Count];
				for (int i = 0; i < originalVertices.Count; i++) {
					float distance = (tectonicPlate - originalVertices [i]).magnitude * 10;
					if(distance < 0.5f * animationTime)
						vertices [i] = originalVertices [i] + new Vector3 (0, 0, Random.Range(0.0f, 1.0f) / (1 + Mathf.Pow(distance, 2)));
					else
						vertices[i] = originalVertices[i];
				}

				GetComponent<MeshFilter> ().mesh.vertices = vertices;
				GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
			}

			animationTime += Time.deltaTime;
			earthquakeAnimTime += Time.deltaTime;
		}
		else if (animationTime > warningTime && animationTime < warningTime + 1.0f) {
			Vector3[] vertices = new Vector3[originalVertices.Count];
			Vector3[] currentVertices = GetComponent<MeshFilter> ().mesh.vertices;
			for (int i = 0; i < originalVertices.Count; i++) {
				float distance = (tectonicPlate - originalVertices [i]).magnitude * 10;
				vertices [i] = currentVertices [i] - new Vector3 (0, 0, 0.5f / (1 + Mathf.Pow (distance, 8)));
			}

			GetComponent<MeshFilter> ().mesh.vertices = vertices;
			GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
			GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
			GetComponent<MeshCollider> ().sharedMesh = GetComponent<MeshFilter> ().mesh;
			animationTime += Time.deltaTime;
		} else if (animationTime >= warningTime + 1.0f && animationTime < warningTime + 2.0f) {
			Vector3[] vertices = new Vector3[originalVertices.Count];
			Vector3[] currentVertices = GetComponent<MeshFilter> ().mesh.vertices;
			for (int i = 0; i < originalVertices.Count; i++) {
				vertices [i] = currentVertices[i] + (originalVertices[i] - currentVertices[i]) / 10;
			}

			GetComponent<MeshFilter> ().mesh.vertices = vertices;
			GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
			GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
			GetComponent<MeshCollider> ().sharedMesh = GetComponent<MeshFilter> ().mesh;
			animationTime += Time.deltaTime;
		} else if (animationTime >= warningTime + 2.0f) {
			animationTime = 0;
			deformation = false;
		}
	}

	void spawnCrate()
	{
		Vector3 min = GetComponent<Renderer> ().bounds.min;
		Vector3 max = GetComponent<Renderer> ().bounds.max;
		Vector3 size = GetComponent<Renderer> ().bounds.size;
		Instantiate (cratePrefab, new Vector3 (Random.Range(min.x + size.x * 0.1f, max.x - size.x * 0.1f), 50, Random.Range(min.z + size.z * 0.1f, max.z - size.z * 0.1f)), Quaternion.identity);
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
