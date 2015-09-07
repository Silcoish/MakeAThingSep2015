using UnityEngine;
using System.Collections;

public class Terrain : MonoBehaviour {

	public float speedModValue = 1.0f;

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			col.GetComponent<Vehicle>().SetTerrainModifier(speedModValue);
		}
	}
}
