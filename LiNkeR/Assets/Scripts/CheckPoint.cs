using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public int id;
	public bool isFinishLine = false;

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			if(id == 0)
				return;

			Vehicle tempV = col.GetComponent<Vehicle>();
			//print (tempV.checkPointID % (GameManager.inst.checkPointsPerLap - 1) + " == " + (id - 1));
			if(tempV.checkPointID % (GameManager.inst.checkPointsPerLap - 1) == (id - 1))
			{
				tempV.checkPointID++;
				if(isFinishLine)
				{
					tempV.lap++;
				}
			}
		}
	}
}
