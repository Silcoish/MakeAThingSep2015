using UnityEngine;
using System.Collections;

public class SpeedBooster : MonoBehaviour {

	[SerializeField] float boostModifier = 1.0f;
	[SerializeField] float boostTime = 1.0f;

	public void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			col.GetComponent<Vehicle>().SetBoostSpeed(boostModifier, boostTime);
		}
	}
}
