using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ocean : MonoBehaviour {
	float animationTime;
	List<Vector3> originalVertices;
	List<Vector2> originalUV;
	private GravitySource gSource;
	public bool deformation;
	Vector3 tectonicPlate;
	public Vector3 disasterPoint;
	public float warningTime;
	public float warningStep;
	private float earthquakeAnimTime;

	// Use this for initialization
	void Start () {
		int resolution = 50;

		MeshFilter meshFilter = GetComponent<MeshFilter> ();
		meshFilter.mesh = new Mesh ();

		originalVertices = new List<Vector3> ();
		originalUV = new List<Vector2> ();
		List<Vector3> vertices = new List<Vector3> ();
		List<Color> colors = new List<Color> ();
		List<Vector2> uv = new List<Vector2> ();
		List<int> triangles = new List<int> ();

		for(int j = 0; j < resolution; j++)
		{
			for (int i = 0; i <= resolution; i++) {
				vertices.Add (new Vector3 (((float)i / (float)resolution) - 0.5f, ((float)j / (float)resolution) - 0.5f, 0));
				uv.Add (new Vector2((float)i / (float)resolution, (float)j / (float)resolution));
				originalVertices.Add (vertices[vertices.Count - 1]);
				originalUV.Add (uv [uv.Count - 1]);

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

		GetComponent<MeshCollider> ().sharedMesh = meshFilter.mesh;

		animationTime = 0;
		deformation = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2[] uv = new Vector2[originalUV.Count];
		Vector3[] vertices = new Vector3[originalVertices.Count];
		Vector3[] currentVertices = GetComponent<MeshFilter> ().mesh.vertices;

		for (int i = 0; i < originalUV.Count; i++) {
			uv [i] = originalUV [i] + 0.02f * new Vector2(originalVertices [i].x, originalVertices[i].y).normalized * Mathf.Sin (0.2f * originalVertices[i].magnitude - 0.2f * Mathf.PI * Time.realtimeSinceStartup);
			Vector3 normal = Vector3.Cross (originalVertices [i].normalized, new Vector3 (0, 1, 0));
			uv [i] = uv [i] + 0.01f * new Vector2(normal.x, normal.z) * Mathf.Sin (0.002f * originalVertices[i].magnitude - 1.5f * Mathf.PI * Time.realtimeSinceStartup);
		}

		if (animationTime == 0.0f && deformation) {
			animationTime += Time.deltaTime;
			tectonicPlate = transform.InverseTransformPoint(disasterPoint);
			gSource = new GravitySource (new GameObject("GravityCenter"), 1500);
			gSource.source.transform.position = transform.TransformPoint (tectonicPlate) - new Vector3 (0, 5, 0);
			gSource.source.transform.SetParent (transform);
			GravitationalForces.gravityCenters.Add (gSource);
		}

		if (animationTime > 0.0f && animationTime < warningTime) {
			transform.position = new Vector3 (transform.position.x, -Mathf.Sin (animationTime / 10), transform.position.z);
			if (earthquakeAnimTime > warningStep) {
				earthquakeAnimTime = 0;

				vertices = new Vector3[originalVertices.Count];
				for (int i = 0; i < originalVertices.Count; i++) {
					vertices [i] = originalVertices [i];
					float distance = (tectonicPlate - originalVertices [i]).magnitude * 50;
					Vector3 diff = currentVertices [i] - tectonicPlate;
					diff = Quaternion.Euler (0, 0, 1.0f / (1.0f + Mathf.Pow(distance, 4))) * diff;
					diff += tectonicPlate;
					uv [i] = uv [i] + new Vector2(tectonicPlate.x, tectonicPlate.z);
				}

				GetComponent<MeshFilter> ().mesh.vertices = vertices;
				GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
			}

			animationTime += Time.deltaTime;
			earthquakeAnimTime += Time.deltaTime;
		} else if (animationTime > warningTime && animationTime < warningTime + 5.0f) {
			vertices = new Vector3[originalVertices.Count];
			currentVertices = GetComponent<MeshFilter> ().mesh.vertices;
			for (int i = 0; i < originalVertices.Count; i++) {
				float distance = (tectonicPlate - originalVertices [i]).magnitude * 50;
				if (distance < 5) {
					Vector3 diff = currentVertices [i] - tectonicPlate;
					float distance2 = (tectonicPlate - originalVertices [i]).magnitude;
					diff = Quaternion.Euler (0, 0, 1.0f / (1.0f + Mathf.Pow(distance, 4))) * diff;
					vertices [i] = diff + tectonicPlate + new Vector3 (0, 0, 0.1f / (1.0f + Mathf.Pow (distance, 4)));
					diff += tectonicPlate;
					uv [i] = uv [i] + new Vector2(tectonicPlate.x, tectonicPlate.z);
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
		} else if (animationTime >= warningTime + 5.0f && animationTime < warningTime + 10.0f) {

			vertices = new Vector3[originalVertices.Count];
			currentVertices = GetComponent<MeshFilter> ().mesh.vertices;
			for (int i = 0; i < originalVertices.Count; i++) {
				float distance = (tectonicPlate - originalVertices [i]).magnitude * 50;
				if (distance < 5) {
					Vector3 diff = currentVertices [i] - tectonicPlate;
					diff = Quaternion.Euler (0, 0, 4.0f / (1.0f + Mathf.Pow(distance, 4))) * diff;
					vertices [i] = diff + tectonicPlate;
					diff += tectonicPlate;
					uv [i] = uv [i] + new Vector2(tectonicPlate.x, tectonicPlate.z);
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
		else if (animationTime >= warningTime + 10.0f && animationTime < warningTime + 11.0f) {
			vertices = new Vector3[originalVertices.Count];
			currentVertices = GetComponent<MeshFilter> ().mesh.vertices;
			bool done = true;
			for (int i = 0; i < originalVertices.Count; i++) {
				vertices [i] = currentVertices [i] + (originalVertices [i] - currentVertices [i]) / 10;
				uv [i] = uv [i] + (originalUV [i] - uv [i]) / 10;
				if (done && (vertices [i] - originalVertices [i]).magnitude > 0.001f)
					done = false;
			}

			GetComponent<MeshFilter> ().mesh.vertices = vertices;
			GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
			GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
			GetComponent<MeshCollider> ().sharedMesh = GetComponent<MeshFilter> ().mesh;
			animationTime += Time.deltaTime;

			if (!done && animationTime >= warningTime + 11.0f)
				animationTime -= Time.deltaTime;
		}
		else if (animationTime >= warningTime + 6.0f) {
			animationTime = 0;
			deformation = false;
			GravitationalForces.gravityCenters.Remove (gSource);
			Destroy (gSource.source);
		}

		GetComponent<MeshFilter> ().mesh.uv = uv;
	}
}
