using UnityEngine;
using System.Collections;

public class HealthPack : Item{

    public float healAmount = 10f;
	void Start()
    {
        owner.GetComponent<Vehicle>().GiveHealth(healAmount);
        owner.GetComponent<Vehicle>().linkedCar.GetComponent<Vehicle>().GiveHealth(healAmount);
        Destroy(gameObject);
    }
}
