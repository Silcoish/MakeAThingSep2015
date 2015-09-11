using UnityEngine;
using System.Collections;

public class HealthPack : Item{

    public float healAmount = 10f;
    public AudioClip healthSound;

	void Start()
    {
        AudioSource.PlayClipAtPoint(healthSound, Vector2.zero, GameManager.inst.itemVol);
        owner.GetComponent<Vehicle>().GiveHealth(healAmount);
        owner.GetComponent<Vehicle>().linkedCar.GetComponent<Vehicle>().GiveHealth(healAmount);
        Destroy(gameObject);
    }
}
