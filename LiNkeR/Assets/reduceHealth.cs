﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class reduceHealth : MonoBehaviour {

	Slider slider;

	// Use this for initialization
	void Start () 
	{
		slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(slider.value != slider.minValue)
		{
			slider.value -= 0.5f;
		}
	}
}
