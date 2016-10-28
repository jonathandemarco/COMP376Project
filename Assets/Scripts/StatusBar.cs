using UnityEngine;
using System.Collections;

public class StatusBar : MonoBehaviour {
	public float value;
	private float newValue;
	private Vector3 initialScale;
	// Use this for initialization
	void Start () {
		value = 1;
		newValue = 1;
		initialScale = transform.GetChild(0).transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		newValue += (value - newValue) / 20;
		transform.GetChild(0).transform.localScale = new Vector3(initialScale.x, initialScale.y * newValue, initialScale.z);
	}

	public void setValue(float number, float max)
	{
		if (max == 0)
			value = 0;
		else
			value = number / max;
	}
}
