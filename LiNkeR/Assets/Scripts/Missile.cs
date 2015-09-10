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
    public AudioClip missileLaunchSound;
    public AudioClip missileHitSound;

	// Use this for initialization
	void Start () 
	{
        AudioSource.PlayClipAtPoint(missileLaunchSound, Vector2.zero);
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
            AudioSource.PlayClipAtPoint(missileHitSound, Vector2.zero);
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			trailEffect.gameObject.SetActive(false);
			rigid.isKinematic = true;
			explosion.SetActive(true);
			hitSomething = true;
		}
		else if(col.gameObject.tag == "Player" && !hitSomething)
		{
            AudioSource.PlayClipAtPoint(missileHitSound, Vector2.zero);
			GetComponent<BoxCollider2D>().enabled = false;
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			trailEffect.gameObject.SetActive(false);
			rigid.isKinematic = true;
			explosion.SetActive(true);
			hitSomething = true;
			col.gameObject.GetComponent<Vehicle>().linkedCar.GetComponent<Vehicle>().TakeHealth(damage);
			Debug.Log("Do " + damage + " Damage to player" + col.gameObject.GetComponent<Vehicle>().playerID);
		}
	}
}
