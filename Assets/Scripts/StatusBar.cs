﻿using UnityEngine;
using System.Collections;

public class StatusBar : MonoBehaviour {
	public float value;
	private Vector3 initialScale;
	// Use this for initialization
	void Start () {
		value = 1;
		initialScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setValue(float number, float max)
	{
		if (max == 0)
			value = 0;
		else
			value = number / max;

		transform.localScale = initialScale * value;
	}
}
