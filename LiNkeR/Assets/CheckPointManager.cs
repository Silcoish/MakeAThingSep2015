using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckPointManager : MonoBehaviour {

	List<CheckPoint> checkPoints;

	void Start()
	{
		checkPoints = new List<CheckPoint>();

		int childCount = transform.childCount;
		for(int i = 0; i < childCount; i++)
		{
			checkPoints.Add(transform.GetChild(i).GetComponent<CheckPoint>());
			checkPoints[i].id = i;
		}
	}

}
