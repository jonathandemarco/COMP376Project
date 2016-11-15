using UnityEngine;
using System.Collections;

public class Vacuum : Weapon {
	Renderer[] renderers;
	GravitySource gSource;
	bool isSucking;


	public void Start()
	{
		transform.localRotation = Quaternion.Euler (90, 0, 0);
		renderers = gameObject.GetComponentsInChildren<MeshRenderer>();

		gSource = new GravitySource (new GameObject("GravityCenter"), 0);
		gSource.source.transform.SetParent (transform);
		gSource.source.transform.localPosition = new Vector3 (0, 0, 0);
		gSource.forceCalculation = delegate(GravitySource gS, Transform g) {
			Vector3 diff = (gS.source.transform.position - g.transform.position);
			return gS.mass * diff.normalized / (1.0f + diff.sqrMagnitude + Mathf.Pow(4 * Vector3.Angle(gS.source.transform.forward, (g.position - gS.source.transform.position).normalized) * Mathf.Deg2Rad, 4));
		};
		GravitationalForces.gravityCenters.Add (gSource);

		isSucking = false;
	}

	public override void Update()
	{
		if (!isSucking && gSource.mass > 0)
			gSource.mass -= 200.0f * Time.deltaTime;

		if (gSource.mass < 0)
			gSource.mass = 0;
	}
	public override void PressAttack(ControlButton button) {
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = true;
		}

		isSucking = true;
	}

	public override void ReleaseAttack (ControlButton button) 
	{
		Debug.Log ("Released");

		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = false;
		}

		isSucking = false;
	}

	public override void HoldAttack (ControlButton button)
	{
		if(gSource.mass < 800)
			gSource.mass += 100.0f * Time.deltaTime;
	}

	public void OnDestroy()
	{
//		for (int i = 0; i < gSources.Length; i++)
//			GravitationalForces.gravityCenters.Remove (gSources [i]);
	}
}
