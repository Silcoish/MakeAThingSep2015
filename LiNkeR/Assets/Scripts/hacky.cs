using UnityEngine;
using System.Collections;

public class hacky : MonoBehaviour {

	public GameObject cars;
	public Transform[] carPos;
	
	// Update is called once per frame
	void Start()
	{
		carPos = new Transform[cars.transform.childCount];
		for(int i = 0; i < cars.transform.childCount; i++)
		{
			carPos[i] = cars.transform.GetChild(i);
		}
	}

	void Update () {
		/*float minX = 99999;
		float maxX = 0;
		float minY = 99999;
		float maxY = 0;

		for(int i = 0; i < carPos.Length; i++)
		{
			if(carPos[i].position.x < minX)
				minX = carPos[i].position.x;
			if(carPos[i].position.x > maxX)
				maxX = carPos[i].position.x;
		}

		Camera.main.orthographicSize = (maxX - minX);*/
		transform.position = new Vector3(carPos[0].position.x, carPos[0].position.y, -10f);
	}
}
