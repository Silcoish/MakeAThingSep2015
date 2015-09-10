using UnityEngine;
using System.Collections;

public class ItemBlock : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			col.gameObject.GetComponent<Vehicle>().item = GameManager.inst.itemList[Random.Range(0, GameManager.inst.itemList.Count)];
            GameManager.inst.itemIconLocations[col.gameObject.GetComponent<Vehicle>().playerID].sprite = col.gameObject.GetComponent<Vehicle>().item.GetComponent<SpriteRenderer>().sprite;
			//StartCoroutine(PickItem(col.gameObject));
			Destroy(gameObject);
		}
	}

	IEnumerator PickItem(GameObject player)
	{
		yield return new WaitForSeconds(1);
		player.GetComponent<Vehicle>().item = GameManager.inst.itemList[Random.Range(0, GameManager.inst.itemList.Count)];
  
        
	}
}
