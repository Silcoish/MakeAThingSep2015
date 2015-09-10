using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class CharacterManager : MonoBehaviour {

	public List<GameObject> characters;
	public List<bool> characterAvaliable;
	public List<GameObject> characterPositions;

	public int[] characterID = new int[4];
	public GamePadState[] prevState = new GamePadState[4];
	public GamePadState[] curState = new GamePadState[4];
	public bool[] hasSelected = new bool[4];

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	void Start()
	{
		if (GameObject.Find("PlayerDirection"))
		{
			ChangeCharacterOrientation();
		}

		characterAvaliable = new List<bool>();
		for(int i = 0; i < characters.Count; i++)
			characterAvaliable.Add(true);

		for(int i = 0; i < 4; i++)
		{
			characterPositions[i].GetComponent<SpriteRenderer>().sprite = characters[i].GetComponent<Character>().characterSprite;
			characterID[i] = i;

			prevState[i] = GamePad.GetState((PlayerIndex)i);
			curState[i] = GamePad.GetState((PlayerIndex)i);
		}
	}

	void Update()
	{
        if(characterPositions[0] != null)
        {
            for (int i = 0; i < 4; i++)
            {
                prevState[i] = curState[i];
                curState[i] = GamePad.GetState((PlayerIndex)i);

                if (!curState[i].IsConnected)
                {

                }

                //Move LEFT
                if (curState[i].ThumbSticks.Left.X < -0.5 && prevState[i].ThumbSticks.Left.X > -0.5)
                {
                    if (!hasSelected[i])
                        ChangeCharacter(i, true);
                } //Move RIGHT
                else if (curState[i].ThumbSticks.Left.X > 0.5 && prevState[i].ThumbSticks.Left.X < 0.5)
                {
                    if (!hasSelected[i])
                        ChangeCharacter(i, false);
                }

                if (prevState[i].Buttons.A == ButtonState.Pressed && curState[i].Buttons.A == ButtonState.Released)
                {
                    if (characterAvaliable[characterID[i]])
                    {
                        //Play Sound
                        AudioSource.PlayClipAtPoint(characters[characterID[i]].GetComponent<Character>().PlaySelect(), Vector2.zero);
                        characterAvaliable[characterID[i]] = false;
                        hasSelected[i] = true;
                        CheckCharactersColors();
                    }
                }

            }
        }
	}

	void ChangeCharacterOrientation()
	{
		GameObject tempGO = GameObject.Find("PlayerDirection");
		PersistentDirection pd = tempGO.GetComponent<PersistentDirection>();

		for(int i = 0; i < 4; i++)
		{
			switch(pd.directions[i])
			{
				case Vehicle.SittingDirection.BOTTOM:
					characterPositions[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
					break;
				case Vehicle.SittingDirection.TOP:
					characterPositions[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
					break;
				case Vehicle.SittingDirection.LEFT:
					characterPositions[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
					break;
				case Vehicle.SittingDirection.RIGHT:
					characterPositions[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
					break;
			}
		}
	}

	void ChangeCharacter(int player, bool left)
	{
		if(left)
		{
			characterID[player]--;
			if(characterID[player] < 0)
				characterID[player] = characters.Count - 1;
		}
		else
		{
			characterID[player]++;
			if(characterID[player] >= characters.Count)
				characterID[player] = 0;
		}

		characterPositions[player].GetComponent<SpriteRenderer>().sprite = characters[characterID[player]].GetComponent<Character>().characterSprite;

		CheckCharactersColors();
	}

	void CheckCharactersColors()
	{
		for(int i = 0; i < 4; i++)
		{
			if(!characterAvaliable[characterID[i]])
			{
				characterPositions[i].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
			}
			else
			{
				characterPositions[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			}
		}

        if (AllSelected())
            Application.LoadLevel("corey");
	}

	bool AllSelected()
    {
        for(int i = 0; i < 4; i++)
        {
            if (!hasSelected[i])
                return false;
        }

        return true;
    }
}
