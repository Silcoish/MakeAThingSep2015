using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinScene : MonoBehaviour {

    public List<Sprite> charSprites = new List<Sprite>();
    public List<SpriteRenderer> winSprites = new List<SpriteRenderer>();
    public TextMesh winText;
    public List<string> winStrings = new List<string>();
    public List<int> winID = new List<int>();
    public List<Color> bgColours = new List<Color>();
    public SpriteRenderer bgSprite;
    public List<AudioClip> winLines = new List<AudioClip>();
    public List<AudioClip> winMusic = new List<AudioClip>();
    public AudioSource soundMaker;

	void Start () 
    {
        winID[0] = PlayerPrefs.GetInt("firstPlace");
        winID[1] = PlayerPrefs.GetInt("secondPlace");
        winID[2] = PlayerPrefs.GetInt("thirdPlace");
        for (int i = 0; i < winSprites.Count; i++) 
        {
            winSprites[i].sprite = charSprites[winID[i]];
        }

        winText.text = winStrings[winID[0]] + " WINS!";
        bgSprite.color = bgColours[winID[0]];
       
        soundMaker.clip = winMusic[winID[0]];
        StartCoroutine(WinDelay());
	}
	
	void Update () 
    {
	
	}

    IEnumerator WinDelay() 
    {
        yield return new WaitForSeconds(0.3f);
        AudioSource.PlayClipAtPoint(winLines[winID[0]], Vector2.zero);
        yield return new WaitForSeconds(2f);
        soundMaker.Play();
    }
}
