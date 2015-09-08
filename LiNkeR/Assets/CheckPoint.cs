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
			if(tempV.checkPointID == (id - 1))
			{
				tempV.checkPointID = id;
				if(isFinishLine)
				{
					tempV.lap++;
					tempV.checkPointID = 0;
				}
			}
		}
	}
}
