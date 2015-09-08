using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager inst;
	public GameObject checkPointManager;
	public GameObject carsParent;
	Vehicle[] racePositions = new Vehicle[4];

	void Start () {
		if(GameManager.inst == null)
			GameManager.inst = this;
		else
			Destroy(this);
	}

	void Update()
	{
		DetermineRacePositions();
	}

	void DetermineRacePositions()
	{
		if(carsParent.transform.GetChild(0).GetComponent<Vehicle>() != null)
		{
			racePositions = new Vehicle[4];
			for(int i = 0; i < 4; i++)
			{
				racePositions[i] = carsParent.transform.GetChild(i).GetComponent<Vehicle>();
			}

			bool sorted = false;
			while(!sorted)
			{
				sorted = true;
				for(int i = 0; i < 4; i++)
				{
					if(i != 0)
					{
						if(racePositions[i].checkPointID > racePositions[i - 1].checkPointID)
						{
							Vehicle temp = racePositions[i];
							racePositions[i] = racePositions[i - 1];
							racePositions[i - 1] = temp;
							sorted = false;
						}
						else if(racePositions[i].checkPointID == racePositions[i - 1].checkPointID)
						{
							float dis  = Vector2.Distance(racePositions[i].transform.position, checkPointManager.transform.GetChild(racePositions[i].checkPointID).position);
							float dis1 = Vector2.Distance(racePositions[i - 1].transform.position, checkPointManager.transform.GetChild(racePositions[i - 1].checkPointID).position);

							if(dis < dis1)
							{
								Vehicle temp = racePositions[i];
								racePositions[i] = racePositions[i - 1];
								racePositions[i - 1] = temp;
								sorted = false;
							}
						}
					}
				}
			}
		}
	}
}
