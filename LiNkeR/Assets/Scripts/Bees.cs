using UnityEngine;
using System.Collections;

public class Bees : Item {

	Vehicle hitPlayer;
	bool playerHit = false;
	float counter;

	void Update () 
	{
		if(playerHit)
		{
			counter += Time.deltaTime;
			transform.position = hitPlayer.transform.position;
			if(counter > 4)
			{
				DestroyObject(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			hitPlayer = col.gameObject.GetComponent<Vehicle>();
			playerHit = true;
		}
	}

}
