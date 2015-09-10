using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager inst;

	public GameObject checkPointManager;
	public int checkPointsPerLap;
	public GameObject carsParent;
	Vehicle[] racePositions = new Vehicle[4];
    public Image[] itemIconLocations = new Image[4];
    public Sprite defaultItemIcon;

	public List<GameObject> itemList;
	bool set = false;

	void Awake () {
		if(GameManager.inst == null)
			inst = this;
		else
			Destroy(this);
	}

	void Update()
	{
		if(!set)
		{
			if(carsParent.transform.GetChild(0).GetComponent<Vehicle>() != null)
			{
				racePositions = new Vehicle[4];
				for(int i = 0; i < 4; i++)
				{
					racePositions[i] = carsParent.transform.GetChild(i).GetComponent<Vehicle>();
                    if(GameObject.Find("PlayerDirection") != null)
                    {
                        carsParent.transform.GetChild(i).GetComponent<Vehicle>().sittingDir = GameObject.Find("PlayerDirection").GetComponent<PersistentDirection>().directions[i];
                    }
				}
				set = true;
			}
		}
		else
		{
			DetermineRacePositions();
		}
	}

	void DetermineRacePositions()
	{
		if(carsParent.transform.GetChild(0).GetComponent<Vehicle>() != null)
		{

			bool sorted = false;
			int counter = 0;
			while(!sorted)
			{
				counter++;
				if(counter > 100)
				{
					print ("Early exit");
					return;
				}

				sorted = true;
				for(int i = 0; i < 4; i++)
				{
					if(i != 0)
					{

						if(racePositions[i].checkPointID == racePositions[i - 1].checkPointID)
						{
							float dis  = Vector2.Distance(racePositions[i].transform.position, checkPointManager.transform.GetChild((racePositions[i].checkPointID % (GameManager.inst.checkPointsPerLap - 1)) + 1).position);
							float dis1 = Vector2.Distance(racePositions[i - 1].transform.position, checkPointManager.transform.GetChild((racePositions[i - 1].checkPointID % (GameManager.inst.checkPointsPerLap - 1)) + 1).position);

							if(dis < dis1)
							{
								Vehicle temp = racePositions[i];
								racePositions[i] = racePositions[i - 1];
								racePositions[i - 1] = temp;
								sorted = false;
							}
						}

						if(racePositions[i].checkPointID > racePositions[i - 1].checkPointID)
						{
							Vehicle temp = racePositions[i];
							racePositions[i] = racePositions[i - 1];
							racePositions[i - 1] = temp;
						}

						/*if(racePositions[i].lap > racePositions[i - 1].lap)
						{
							Vehicle temp = racePositions[i];
							racePositions[i] = racePositions[i - 1];
							racePositions[i - 1] = temp;
							sorted = false;
						}*/
					}
				}
			}

			for(int j = 0; j < 4; j++)
			{
				racePositions[j].positionText.text = (j + 1).ToString();
			}
		}
	}

    public void SetDefaultIcon(int player)
    {
        itemIconLocations[player].sprite = defaultItemIcon;
    }
}
