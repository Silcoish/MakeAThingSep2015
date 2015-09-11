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
	void Awake () 
	{
        AudioSource.PlayClipAtPoint(missileLaunchSound, Vector2.zero);
		rigid = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(!hitSomething)
		{
			rigid.AddForce(-transform.right * speed);
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
			print ("Hit Wall");
            AudioSource.PlayClipAtPoint(missileHitSound, Vector2.zero, GameManager.inst.itemVol);
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			trailEffect.gameObject.SetActive(false);
			rigid.isKinematic = true;
			explosion.SetActive(true);
			hitSomething = true;
		}
		else if(col.gameObject.tag == "Player" && !hitSomething)
		{
			print("Collided with a player");
            AudioSource.PlayClipAtPoint(missileHitSound, Vector2.zero, GameManager.inst.itemVol);
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
