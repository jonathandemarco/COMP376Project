using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotDemosLevel : LevelManager {
	public float meteorProb;
	public float deformationProb;
	public List<GameObject> disasterPoints;
	public GameObject disasterPtPrefab;
	public GameObject[] palmTreePrefabs;
	public GameObject spawnPointPrefab;
	List<Vector3> originalVertices;
	List<GameObject> palmTrees;

	public int samples = 1000;
	public int radius = 5;
	public int bRadius = 5;
	public float maxHeight = 0.5f;
	// Use this for initialization

	override public void Start () {
		
		setUpMesh (50);

		initialSpawnsList = new List<Vector3> ();
		allSpawnsList = new List<Vector3> ();

		for (int i = 0; i < palmTrees.Count; i++) {
			{
				bool found = true;
				for(int j = 0; j < allSpawnsList.Count; j++)
				{
					if ((palmTrees [i].transform.position - allSpawnsList [j]).magnitude < 20) {
						found = false;
						break;
					}
				}

				if (found) {
					GameObject o = Instantiate (spawnPointPrefab, palmTrees [i].transform.position + new Vector3(2.0f, 10.0f, 0), Quaternion.identity, transform) as GameObject;
					o.name = "SpawnPoint";
				}
			}
		}

		base.Start ();
	}
	
	// Update is called once per frame
	override public void Update () {
		base.Update ();

		if (1 - Random.Range (0.0f, 1.0f) < meteorProb)
			spawnMeteorShower ();

		if (1 - Random.Range (0.0f, 1.0f) < deformationProb) {
			Ocean ocean = GetComponentInChildren<Ocean> ();
			ocean.disasterPoint = disasterPoints [Random.Range (0, disasterPoints.Count)].transform.position;
			ocean.deformation = true;
		}
	}

	void setUpMesh(int resolution)
	{
		float increment = maxHeight / (float)samples;

		float[,] heightMap = new float[resolution + 1, resolution + 1];
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

		palmTrees = new List<GameObject> ();
		disasterPoints = new List<GameObject> ();


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
				colors.Add (Color.Lerp(new Color(1.0f, 1.0f, 0.0f, 1.0f), Color.green, Mathf.Pow(Mathf.Abs((heightMap [i, j] - avgHeight) * 8.0f) / max, 6)));

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

				Vector3 currPt = transform.TransformPoint (vertices [vertices.Count - 1]);

				if (colors [colors.Count - 1].r < 0.1f && currPt.y > 0) {
					bool secluded = true;

					for(int k = 0; k < palmTrees.Count; k++)
					{
						if ((currPt - palmTrees [k].transform.position).magnitude < 5.0f) {
							secluded = false;
							break;
						}
					}

					if (secluded) {
						palmTrees.Add (Instantiate (palmTreePrefabs [Random.Range (0, palmTreePrefabs.Length)], currPt, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0), transform) as GameObject);
						palmTrees [palmTrees.Count - 1].transform.localScale /= 200.0f;
					}
				}
					
				if (colors [colors.Count - 1].r > 0.98f && currPt.y >= 0 && i > resolution * 0.1f && j > resolution * 0.1f && i < resolution * 0.9f && j < resolution * 0.9f) {
					disasterPoints.Add (Instantiate(disasterPtPrefab, currPt, Quaternion.identity, transform) as GameObject);
				}

				if (colors [colors.Count - 1].r > 0.8)
					colors [colors.Count - 1] = new Color32(0, 0, 0, 0);
			}
		}
			
		meshFilter.mesh.vertices = vertices.ToArray();
		meshFilter.mesh.colors = colors.ToArray ();
		meshFilter.mesh.uv = uv.ToArray();
		meshFilter.mesh.triangles = triangles.ToArray();
		meshFilter.mesh.RecalculateBounds ();
		meshFilter.mesh.RecalculateNormals ();
	}

	void spawnMeteorShower() {
/*		Vector3 min = GetComponent<Renderer> ().bounds.min;
		Vector3 max = GetComponent<Renderer> ().bounds.max;
		Vector3 size = GetComponent<Renderer> ().bounds.size;
		MeteorShower script = (MeteorShower) Instantiate (meteorShowerPrefab).GetComponent(typeof (MeteorShower));
		script.setCastPoint (new Vector3 (Random.Range (min.x + size.x * 0.1f, max.x - size.x * 0.1f), 0, Random.Range (min.z + size.z * 0.1f, max.z - size.z * 0.1f)));
*/	}
}
