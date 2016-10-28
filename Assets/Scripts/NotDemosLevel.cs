using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotDemosLevel : LevelManager {

	// Use this for initialization

	override public void Start () {
		base.Start ();

		GetComponent<MeshFilter> ().mesh = new Mesh ();

		List<Vector3> vertices = new List<Vector3> ();
		List<int> triangles = new List<int> ();

		int resolution = 10;

		for(int j = 0; j <= resolution; j++)
		{
			for (int i = 0; i <= resolution; i++) {
				vertices.Add (new Vector3 (((float)i / (float)resolution) - 0.5f, ((float)j / (float)resolution) - 0.5f, 0.0f));

				if (i > 0 && j > 0) {
					triangles.Add (vertices.Count - 2);
					triangles.Add (vertices.Count - 1);
					triangles.Add (vertices.Count - resolution - 2);

					triangles.Add (vertices.Count - 2);
					triangles.Add (vertices.Count - resolution - 1);
					triangles.Add (vertices.Count - resolution - 3);
				}
			}
		}
		GetComponent<MeshFilter> ().mesh.vertices = vertices.ToArray();
		GetComponent<MeshFilter> ().mesh.triangles = triangles.ToArray();

		GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
		GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
	}
	
	// Update is called once per frame
	override public void Update () {
		//base.Update ();
	}
}
