using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	public float timer;
	public float speed;
	public GameObject explosion;
	Rigidbody2D rigid;
	float counter;
	bool Exploded = false;

	void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
	}

	void Update () 
	{
		if(!Exploded && counter > timer)
		{
			print ("Timeout");
			Explode();
		}
		else
		{
			counter += Time.deltaTime;
			rigid.AddForce(transform.right * speed);
		}
	}

	void Explode()
	{
		Exploded = true;
		explosion.SetActive(true);
		rigid.isKinematic = true;
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<CircleCollider2D>().enabled = false;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player" || col.gameObject.tag == "Wall")
		{
			if(!Exploded)
			{
				print ("Collided");
				Explode();
			}
		}
	}

}
