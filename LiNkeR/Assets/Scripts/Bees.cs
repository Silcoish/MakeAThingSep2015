using UnityEngine;
using System.Collections;

public class Bees : Item {

	Vehicle hitPlayer;
	bool playerHit = false;
	float counter;

    public AudioClip beesDeploySound;
    public AudioClip beesHit;

    void Start()
    {
        AudioSource.PlayClipAtPoint(beesDeploySound, Vector2.zero);
    }

	void Update () 
	{
		globalCounter += Time.deltaTime;
		if(globalCounter > globalCooldown)
			canAttackOwner = true;

		if(playerHit)
		{
			if(hitPlayer.gameObject == owner && !canAttackOwner)
			{
				playerHit = false;
				hitPlayer = null;
				return;
			}
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
            AudioSource.PlayClipAtPoint(beesHit, Vector2.zero);
			hitPlayer = col.gameObject.GetComponent<Vehicle>();
			playerHit = true;
		}
	}

}
