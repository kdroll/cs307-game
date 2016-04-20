﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TheBossHealthBar : MonoBehaviour {

	public float fillAmount;

	public Image content;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		handleBar ();
	}


	void handleBar() {
		content.fillAmount = Map(TheBossHealth.enemyHealth, 0f, TheBossHealth.startHealth, 0f,1f);
	}

	private float Map(float value, float inMin, float inMax, float outMin, float outMax) {
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
