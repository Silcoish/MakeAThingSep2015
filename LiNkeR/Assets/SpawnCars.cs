using UnityEngine;
using System.Collections;

public class SpawnCars : MonoBehaviour {

	public GameObject[] cars;
	void Awake()
	{
		int childCount = transform.childCount;
		GameObject[] carsToSend = new GameObject[childCount];
		for(int i = 0; i < childCount; i++)
		{
			GameObject tempChild = (GameObject)Instantiate(cars[i], transform.GetChild(i).transform.position, Quaternion.identity);
			tempChild.transform.parent = gameObject.transform;
			Vehicle v = tempChild.GetComponent<Vehicle>(); 
			v.playerID = i;
			float pitchVariation = Random.Range(-0.4f, 0.4f);
			v.minEnginePitch += pitchVariation;
			v.maxEnginePitch += pitchVariation;
			carsToSend[i] = tempChild;
		}

		int index = Random.Range(1, 4);
		carsToSend[0].GetComponent<Vehicle>().linkedCar = carsToSend[index];
		carsToSend[index].GetComponent<Vehicle>().linkedCar = carsToSend[0];
	
		if(index == 1)
		{
			carsToSend[2].GetComponent<Vehicle>().linkedCar = carsToSend[3];
			carsToSend[3].GetComponent<Vehicle>().linkedCar = carsToSend[2];
		}
			
		if(index == 2)
		{
			carsToSend[1].GetComponent<Vehicle>().linkedCar = carsToSend[3];
			carsToSend[3].GetComponent<Vehicle>().linkedCar = carsToSend[1];
		}

		if(index == 3)
		{
			carsToSend[1].GetComponent<Vehicle>().linkedCar = carsToSend[2];
			carsToSend[2].GetComponent<Vehicle>().linkedCar = carsToSend[1];
		}
			

		for(int j = 0; j < childCount; j++)
		{
			Destroy(transform.GetChild(j).gameObject);
		}

		print (carsToSend.Length);
		Camera.main.GetComponent<CameraManager>().SetUp(carsToSend);

	}

}