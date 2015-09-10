using UnityEngine;
using System.Collections;

public class Bubble : Item
{
	public float timeOnTrack;
	float counter = 0;
    public AudioClip bubbleDeploySound;
    public AudioClip bubbleHitSound;
	
    void Start()
    {
        AudioSource.PlayClipAtPoint(bubbleDeploySound, Vector2.zero);
    }

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
            col.gameObject.GetComponent<Vehicle>().acceleration = 0f;
            AudioSource.PlayClipAtPoint(bubbleHitSound, Vector2.zero);
            Destroy(gameObject);
		}
	}
}
