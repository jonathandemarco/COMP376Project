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
	private GravitySource gSource;
	bool deformation = false;
	Vector3 tectonicPlate;

	public int samples = 1000;
	public int radius = 5;
	public int bRadius = 5;
	public float maxHeight = 0.5f;
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

		if (1 - Random.Range (0.0f, 1.0f) < deformationProb)
			deformation = true;

		updateSkybox ();

		if (animationTime == 0.0f && deformation) {
			animationTime += Time.deltaTime;
			tectonicPlate = new Vector3 (Random.Range (-0.5f, 0.5f), Random.Range (-0.5f, 0.5f), 0);
			gSource = new GravitySource (new GameObject("GravityCenter"), 900);
			gSource.source.transform.position = transform.TransformPoint (tectonicPlate) - new Vector3 (0, 5, 0);
			gSource.source.transform.SetParent (transform);
			GravitationalForces.gravityCenters.Add (gSource);
		}

		if (animationTime > 0.0f && animationTime < warningTime) {
			transform.position = new Vector3 (transform.position.x, -Mathf.Sin (animationTime / 10), transform.position.z);
			if (earthquakeAnimTime > warningStep) {
				earthquakeAnimTime = 0;

				Vector3[] vertices = new Vector3[originalVertices.Count];
				for (int i = 0; i < originalVertices.Count; i++) {
//					float distance = (tectonicPlate - originalVertices [i]).magnitude * 10;
//					if(distance < 0.5f * animationTime)
//						vertices [i] = originalVertices [i] + new Vector3 (0, 0, Random.Range(0.0f, 1.0f) / (1 + Mathf.Pow(distance, 2)));
//					else
					vertices [i] = originalVertices [i];
				}

				GetComponent<MeshFilter> ().mesh.vertices = vertices;
				GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
			}

			animationTime += Time.deltaTime;
			earthquakeAnimTime += Time.deltaTime;
		} else if (animationTime > warningTime && animationTime < warningTime + 1.0f) {
			Vector3[] vertices = new Vector3[originalVertices.Count];
			Vector3[] currentVertices = GetComponent<MeshFilter> ().mesh.vertices;
			for (int i = 0; i < originalVertices.Count; i++) {
				float distance = (tectonicPlate - originalVertices [i]).magnitude * 50;
				if (distance < 15) {
					Vector3 diff = currentVertices [i] - tectonicPlate;
					float distance2 = (tectonicPlate - originalVertices [i]).magnitude;
					diff = Quaternion.Euler (0, 0, 20.0f / (0.1f + Mathf.Pow(distance, 4))) * diff;
					vertices [i] = diff + tectonicPlate + new Vector3 (0, 0, 1.0f / (1.0f + Mathf.Pow (distance, 2)));
				} else {
					vertices [i] = currentVertices [i];
				}
//				vertices [i] = currentVertices [i] - new Vector3 (0, 0, 0.5f / (1 + Mathf.Pow (distance, 8)));
			}

			GetComponent<MeshFilter> ().mesh.vertices = vertices;
			GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
			GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
			GetComponent<MeshCollider> ().sharedMesh = GetComponent<MeshFilter> ().mesh;
			animationTime += Time.deltaTime;
		} else if (animationTime >= warningTime + 1.0f && animationTime < warningTime + 5.0f) {
			
			Vector3[] vertices = new Vector3[originalVertices.Count];
			Vector3[] currentVertices = GetComponent<MeshFilter> ().mesh.vertices;
			for (int i = 0; i < originalVertices.Count; i++) {
				float distance = (tectonicPlate - originalVertices [i]).magnitude * 50;
				if (distance < 15) {
					Vector3 diff = currentVertices [i] - tectonicPlate;
					diff = Quaternion.Euler (0, 0, 20.0f / (1.0f + Mathf.Pow(distance, 4))) * diff;
					vertices [i] = diff + tectonicPlate;
					//				vertices [i] = currentVertices [i] - new Vector3 (0, 0, 0.5f / (1 + Mathf.Pow (distance, 8)));
				} else {
					vertices [i] = currentVertices [i];
				}
			}

			GetComponent<MeshFilter> ().mesh.vertices = vertices;
			GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
			GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
			GetComponent<MeshCollider> ().sharedMesh = GetComponent<MeshFilter> ().mesh;

			animationTime += Time.deltaTime;
		}
		else if (animationTime >= warningTime + 5.0f && animationTime < warningTime + 6.0f) {
			Vector3[] vertices = new Vector3[originalVertices.Count];
			Vector3[] currentVertices = GetComponent<MeshFilter> ().mesh.vertices;
			for (int i = 0; i < originalVertices.Count; i++) {
				vertices [i] = currentVertices [i] + (originalVertices [i] - currentVertices [i]) / 10;
			}

			GetComponent<MeshFilter> ().mesh.vertices = vertices;
			GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
			GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
			GetComponent<MeshCollider> ().sharedMesh = GetComponent<MeshFilter> ().mesh;
			animationTime += Time.deltaTime;
		}
		else if (animationTime >= warningTime + 6.0f) {
			animationTime = 0;
			deformation = false;
			GravitationalForces.gravityCenters.Remove (gSource);
			Destroy (gSource.source);
		}
	}

	void setUpMesh(int resolution)
	{
		float increment = maxHeight / (float)samples;

		float[,] heightMap = new float[resolution + 1, resolution + 1];
		//Vector2[] points = {new Vector2((float)resolution / 2, (float)resolution / 5), new Vector2((float)resolution / 5, 4.0f * resolution / 5), new Vector2(4.0f * resolution / 5, 4.0f * resolution / 5) };
		List<Vector2> points = new List<Vector2>();

		for(int a = 0; a < samples; a++)
		{
			Vector2 randomV2 = new Vector3(Random.Range(0, resolution), Random.Range(0, resolution));
				for (int k = Mathf.Max ((int)(randomV2.x - bRadius), 0); k <= Mathf.Min ((int)(randomV2.x + bRadius), resolution); k++)
					for (int l = Mathf.Max ((int)(randomV2.y - bRadius), 0); l <= Mathf.Min ((int)(randomV2.y + bRadius), resolution); l++)
						if (Mathf.Sqrt (Mathf.Pow (k - randomV2.x, 2) + Mathf.Pow (l - randomV2.y, 2)) < bRadius)
							if(Mathf.Abs(heightMap[k, l]) < maxHeight)
								heightMap [k, l] -= increment;
		}


		float avgHeight = 0;
		float max = -Mathf.Infinity;
		float min = Mathf.Infinity;
		for (int i = 0; i < resolution + 1; i++)
			for (int j = 0; j < resolution + 1; j++) {
				avgHeight += heightMap [i, j];
				if (Mathf.Abs(heightMap [i, j]) > max)
					max = Mathf.Abs(heightMap [i, j]);
				if (Mathf.Abs(heightMap [i, j]) < min)
					min = Mathf.Abs(heightMap [i, j]);
			}

		avgHeight /= Mathf.Pow (resolution + 1, 2);

		MeshFilter meshFilter = GetComponent<MeshFilter> ();
		meshFilter.mesh = new Mesh ();

		originalVertices = new List<Vector3> ();
		List<Vector3> vertices = new List<Vector3> ();
		List<Color> colors = new List<Color> ();
		List<Vector2> uv = new List<Vector2> ();
		List<int> triangles = new List<int> ();

		for(int j = 0; j < resolution; j++)
		{
			for (int i = 0; i <= resolution; i++) {
				vertices.Add (new Vector3 (((float)i / (float)resolution) - 0.5f, ((float)j / (float)resolution) - 0.5f, (heightMap [i, j] - avgHeight) * 15));
				colors.Add (Color.Lerp(new Color32(255, 255, 0, 255), Color.green, Mathf.Pow(Mathf.Abs((heightMap [i, j] - avgHeight) * 8.0f) / max, 6)));
				heightMap [i, j] = (heightMap [i, j] - avgHeight) * 15;
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
			
		meshFilter.mesh.vertices = vertices.ToArray();
		meshFilter.mesh.colors = colors.ToArray ();
		meshFilter.mesh.uv = uv.ToArray();
		meshFilter.mesh.triangles = triangles.ToArray();
		meshFilter.mesh.RecalculateBounds ();
		meshFilter.mesh.RecalculateNormals ();

//		GetComponent<MeshCollider> ().sharedMesh = GetComponent<MeshFilter> ().mesh;

		TerrainData newTerrain = new TerrainData ();
		newTerrain.heightmapResolution = resolution + 1;
		newTerrain.SetHeights (0, 0, heightMap);
		GetComponent<TerrainCollider> ().terrainData = newTerrain;
		GetComponent<TerrainCollider> ().enabled = true;
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
