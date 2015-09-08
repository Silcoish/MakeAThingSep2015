using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

	public GameObject cars;
	public Transform[] carPos;
	public float minZoom;
	float currentDistance;
	float largestDistance;
	Camera theCamera;
	float height = 5.0f;
	Vector2 avgDistance;
	float distance = 0.0f;
	float speed = 1;
	float offset;
	Vector2 sum;

	public void SetUp(GameObject[] carsToAdd)
	{
		carPos = new Transform[carsToAdd.Length];
		for (int i = 0; i < cars.transform.childCount; i++)
		{
			carPos[i] = carsToAdd[i].transform;
		}
	}

	void Update()
	{
		if(carPos.Length == 0)
			return;
		sum = new Vector2(0, 0);
		for(int i = 0; i < carPos.Length; i++)
		{
			sum += (Vector2)carPos[i].position;
		}
		avgDistance = sum / carPos.Length;

		float largestDifference = GetLargestDistance();
		height = Mathf.Lerp(height,largestDistance,Time.deltaTime * speed);
		Camera.main.transform.position = new Vector3(GetCamXPos(), GetCamYPos(), -10f) ;
		Camera.main.orthographicSize = GetOrthoSize();

	}

	float GetCamXPos()
	{
		float highest = carPos[0].position.x;
		float lowest = carPos[0].position.x;
		
		for(int i = 0; i < carPos.Length; i++)
		{
			if(carPos[i].position.x > highest)
				highest = carPos[i].position.x;
			if(carPos[i].position.x < lowest)
				lowest = carPos[i].position.x;
		}

		return highest - ((highest - lowest) / 2);
	}

	float GetCamYPos()
	{
		float highest = carPos[0].position.y;
		float lowest = carPos[0].position.y;
		
		for(int i = 0; i < carPos.Length; i++)
		{
			if(carPos[i].position.y > highest)
				highest = carPos[i].position.y;
			if(carPos[i].position.y < lowest)
				lowest = carPos[i].position.y;
		}

		return highest - ((highest - lowest) / 2);
	}

	float GetOrthoSize()
	{
		float highest = carPos[0].position.y;
		float lowest = carPos[0].position.y;
		
		for(int i = 0; i < carPos.Length; i++)
		{
			if(carPos[i].position.y > highest)
				highest = carPos[i].position.y;
			if(carPos[i].position.y < lowest)
				lowest = carPos[i].position.y;
		}

		float retVal = highest - lowest;
		if(retVal < minZoom)
			retVal = minZoom;

		return retVal;
	}

	float GetLargestDistance()
	{
		currentDistance = 0.0f;
		largestDistance = 0.0f;
		for(var i = 0; i < carPos.Length; i++){
			for(var j = 0; j <  carPos.Length; j++){
				currentDistance = Vector2.Distance(carPos[i].position,carPos[j].position);
				if(currentDistance > largestDistance){
					largestDistance = currentDistance;
				}
			}
		}
		return largestDistance;
	}
}
