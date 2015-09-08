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

			if(col.GetComponent<Vehicle>().checkPointID == (id - 1))
			{
				col.GetComponent<Vehicle>().checkPointID = id;
			}
		}
	}
}
