using UnityEngine;
using System.Collections;

public class ItemBlock : MonoBehaviour {

    public float respawnTime = 20f;
    float counter = 0f;
    bool isEnabled = true;

    public AudioClip itemBoxAudio;

    void Update()
    {
        if(!isEnabled)
        {
            counter += Time.deltaTime;
            if (counter > respawnTime)
            {
                isEnabled = true;
                GetComponent<SpriteRenderer>().enabled = true;
                counter = 0;
            }
        }
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player" && isEnabled)
		{
            AudioSource.PlayClipAtPoint(itemBoxAudio, Vector2.zero);
			col.gameObject.GetComponent<Vehicle>().item = GameManager.inst.itemList[Random.Range(0, GameManager.inst.itemList.Count)];
            GameManager.inst.itemIconLocations[col.gameObject.GetComponent<Vehicle>().playerID].sprite = col.gameObject.GetComponent<Vehicle>().item.GetComponent<SpriteRenderer>().sprite;
			//StartCoroutine(PickItem(col.gameObject));
			//Destroy(gameObject);
            isEnabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	IEnumerator PickItem(GameObject player)
	{
		yield return new WaitForSeconds(1);
		player.GetComponent<Vehicle>().item = GameManager.inst.itemList[Random.Range(0, GameManager.inst.itemList.Count)];
  
        
	}
}
