using UnityEngine;
using System.Collections;

public class Bubble : Item
{
	public float timeOnTrack;
	float counter = 0;
	
	// Update is called once per frame
	void Update ()
	{
		if(counter > timeOnTrack)
		{
			Destroy(gameObject);
		}
		else
		{
			counter += Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
	}
}
