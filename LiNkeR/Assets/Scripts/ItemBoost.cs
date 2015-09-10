using UnityEngine;
using System.Collections;

public class ItemBoost : Item {

    public float boostModifier = 1.0f;
    public float boostTime = 3.0f;
	void Start()
    {
        owner.GetComponent<Vehicle>().SetBoostSpeed(boostModifier, boostTime);
        Destroy(gameObject);
    }
}
