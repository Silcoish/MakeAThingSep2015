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
	public Image[] characterIconLocations = new Image[4];
	public Slider[] characterHPLocations = new Slider[4];
	public Text[] characterLapLocations = new Text[4];
    public Sprite defaultItemIcon;

    public Character[] winOrder = new Character[4];
    public int racersComplete = 0;

    public AudioCrossfade audioCrossFade;

    public bool announcedFinalLap = false;
    public AudioClip finalLapSound;

	public List<GameObject> itemList;
	bool set = false;

    [Range(0f, 1f)]
    public float carVol = 1;
    [Range(0f, 1f)]
    public float tauntVol = 1;
    [Range(0f, 1f)]
    public float collisionVol = 1;
    [Range(0f, 1f)]
    public float beeVol = 1;
    [Range(0f, 1f)]
    public float deathVol = 1;
    [Range(0f, 1f)]
    public float hurtVol = 1;
    [Range(0f, 1f)]
    public float winVol = 1;
    [Range(0f, 1f)]
    public float announcerVol = 1;
    [Range(0f, 1f)]
    public float itemVol = 1;

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
                    if (GameObject.Find("CharacterManager") != null)
                    {
                        CharacterManager cm = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
                        carsParent.transform.GetChild(i).GetComponent<Vehicle>().playerCharacter = cm.characters[cm.characterID[i]].GetComponent<Character>();
						carsParent.transform.GetChild(i).FindChild("CarSprite").GetComponent<SpriteRenderer>().sprite = cm.characters[cm.characterID[i]].GetComponent<Character>().carSprite;
						SetCharacterIcons(i);

                    }
				}
				set = true;
			}
		}
		else
		{
			DetermineRacePositions();
			UpdateHP();
			SetCharacterInfo();
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

					}
				}
			}

			for(int j = 0; j < 4; j++)
			{
				racePositions[j].positionText.text = (j + 1).ToString();
			}
		}
	}

	public void SetCharacterIcons(int player)
	{
		characterIconLocations[player].sprite = carsParent.transform.GetChild(player).GetComponent<Vehicle>().playerCharacter.characterIcon;
	}

	public void SetCharacterInfo()
	{
		for(int i = 0; i < 4; i++)
		{
			switch(i)
			{
			case 0:
				characterLapLocations[racePositions[i].playerID].text = "1st";
				break;
			case 1:
				characterLapLocations[racePositions[i].playerID].text = "2nd";
				break;
			case 2:
				characterLapLocations[racePositions[i].playerID].text = "3rd";
				break;
			case 3:
				characterLapLocations[racePositions[i].playerID].text = "4th";
				break;
			}
			switch(racePositions[i].lap)
			{
			case 0:
				characterLapLocations[racePositions[i].playerID].text += " Lap " + racePositions[i].lap + "/3";
				break;
			case 1:
				characterLapLocations[racePositions[i].playerID].text += " Lap " + racePositions[i].lap + "/3";
				break;
			case 2:
				characterLapLocations[racePositions[i].playerID].text += " Lap " + racePositions[i].lap + "/3";
				break;
			case 3:
				characterLapLocations[racePositions[i].playerID].text += " Lap " + racePositions[i].lap + "/3";
				break;
			}
		}
	}

    public void SetDefaultIcon(int player)
    {
        itemIconLocations[player].sprite = defaultItemIcon;
    }

	public void UpdateHP()
	{
		for(int i = 0; i < 4; i++)
		{
			characterHPLocations[i].value = carsParent.transform.GetChild(i).GetComponent<Vehicle>().GetHealth();
		}
	}

    public void SetComplete(Character pc)
    {
        if (racersComplete == 0)
            AudioSource.PlayClipAtPoint(pc.PlayWin(), Vector2.zero);

        winOrder[racersComplete] = pc;

        racersComplete++;
    }

    public void PlayFinalLap()
    {
        if (!announcedFinalLap)
        {
            announcedFinalLap = true;
            AudioSource.PlayClipAtPoint(finalLapSound, Vector2.zero);
        }
    }
}
