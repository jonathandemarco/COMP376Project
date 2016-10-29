using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotDemosLevel : LevelManager {
	List<Vector3> originalVertices;
	float animationTime;
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
		List<int> triangles = new List<int> ();

		int resolution = 50;

		for(int j = 0; j <= resolution; j++)
		{
			for (int i = 0; i <= resolution; i++) {
				vertices.Add (new Vector3 (((float)i / (float)resolution) - 0.5f, ((float)j / (float)resolution) - 0.5f, 0.0f));
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
		GetComponent<MeshFilter> ().mesh.triangles = triangles.ToArray();
		GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
		GetComponent<MeshFilter> ().mesh.RecalculateNormals ();

		GetComponent<MeshCollider> ().sharedMesh = GetComponent<MeshFilter> ().mesh;
	}
	
	// Update is called once per frame
	override public void Update () {
		//base.Update ();
		if (animationTime == 0.0f && Random.Range (0.0f, 1.0f) > 0.99f) {
			animationTime += Time.deltaTime;
			tectonicPlate = new Vector3 (Random.Range (-0.5f, 0.5f), Random.Range (-0.5f, 0.5f), 0);
		}
		
		if (animationTime > 0.0f && animationTime < 1.0f) {
			Vector3[] vertices = new Vector3[originalVertices.Count];
			Vector3[] currentVertices = GetComponent<MeshFilter> ().mesh.vertices;
			for (int i = 0; i < originalVertices.Count; i++) {
				float distance = (tectonicPlate - originalVertices [i]).magnitude * 20;
				vertices [i] = currentVertices [i] - new Vector3 (0, 0, 0.2f / (1 + Mathf.Pow (distance, 16)));
			}

			GetComponent<MeshFilter> ().mesh.vertices = vertices;
			GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
			GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
			GetComponent<MeshCollider> ().sharedMesh = GetComponent<MeshFilter> ().mesh;
			animationTime += Time.deltaTime;
		} else if (animationTime >= 1.0f && animationTime < 2.0f) {
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
		} else if (animationTime >= 2.0f) {
			animationTime = 0;
		}
	}
}
