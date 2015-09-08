using UnityEngine;
using System.Collections;

public class Missile : Item 
{
	public float speed;
	public float damage;
	[SerializeField] GameObject explosion;
	[SerializeField] GameObject trailEffect;
	Rigidbody2D rigid;
	bool hitSomething = false;
	float counter;

	// Use this for initialization
	void Start () 
	{
		rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(!hitSomething)
		{
			rigid.AddForce(transform.right * speed);
		}
		else
		{
			counter += Time.deltaTime;
			if(counter > 0.5)
			{
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Wall")
		{
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			trailEffect.gameObject.SetActive(false);
			rigid.isKinematic = true;
			explosion.SetActive(true);
			hitSomething = true;
		}
		else if(col.gameObject.tag == "Player")
		{
			//col.gameObject.GetComponent<Vehicle>().linkedCar.SendMessage("Take Damage", damage);
			Debug.Log("Do " + damage + " Damage to player" + col.gameObject.GetComponent<Vehicle>().playerID);
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			trailEffect.gameObject.SetActive(false);
			rigid.isKinematic = true;
			explosion.SetActive(true);
			hitSomething = true;
		}
	}
}
