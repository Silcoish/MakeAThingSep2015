using UnityEngine;
using System.Collections;

public class Magnet : Item {

    public float pullforce;
    public float timer = 4f;
    float counter = 0f;

    public void Start()
    {
        transform.parent = owner.GetComponent<Vehicle>().itemInstPoint.transform;
    }

    public void Update()
    {
        counter += Time.deltaTime;
        if (counter > timer)
            Destroy(gameObject);
        for(int i = 0; i < 4; i++)
        {
            GameObject tempCar = GameManager.inst.carsParent.transform.GetChild(i).gameObject;
            if(tempCar != owner)
            {
                Vector2 normalisedDistance = ((Vector2)transform.position - (Vector2)tempCar.transform.position).normalized;
                tempCar.GetComponent<Rigidbody2D>().AddForce(normalisedDistance * pullforce * Time.deltaTime);
            }
        }
    }
}
