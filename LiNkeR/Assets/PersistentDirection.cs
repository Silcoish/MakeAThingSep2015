using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PersistentDirection : MonoBehaviour {

	public Vehicle.SittingDirection[] directions = new Vehicle.SittingDirection[4];
	public bool[] choosen = new bool[4];
	GamePadState[] prevState = new GamePadState[4];
	GamePadState[] curState = new GamePadState[4];

	float timer = 4f;
	float counter = 0.0f;

	public TextMesh[] timerText;

	void Start()
	{
		DontDestroyOnLoad(gameObject);
		for(int i = 0; i < 4; i++)
		{
			prevState[i] = GamePad.GetState((PlayerIndex)i);
			curState[i] = GamePad.GetState((PlayerIndex)i);
		}
	}

	void Update()
	{
		if(timerText[0] != null)
		{
			if (CheckIfReady())
				DoTimer();

			for (int i = 0; i < 4; i++)
			{
				prevState[i] = curState[i];
				curState[i] = GamePad.GetState((PlayerIndex)i);

				if (prevState[i].ThumbSticks.Left.Y < 0.5f && curState[i].ThumbSticks.Left.Y > 0.5f)
				{
					directions[i] = Vehicle.SittingDirection.BOTTOM;
					choosen[i] = true;
				}
				else if (prevState[i].ThumbSticks.Left.Y > -0.5f && curState[i].ThumbSticks.Left.Y < -0.5f)
				{
					directions[i] = Vehicle.SittingDirection.TOP;
					choosen[i] = true;
				}
				else if (prevState[i].ThumbSticks.Left.X < 0.5f && curState[i].ThumbSticks.Left.X > 0.5f)
				{
					directions[i] = Vehicle.SittingDirection.RIGHT;
					choosen[i] = true;
				}
				else if (prevState[i].ThumbSticks.Left.X > -0.5f && curState[i].ThumbSticks.Left.X < -0.5f)
				{
					directions[i] = Vehicle.SittingDirection.LEFT;
					choosen[i] = true;
				}
				else if (prevState[i].Buttons.B == ButtonState.Pressed && curState[i].Buttons.B == ButtonState.Released)
				{
					choosen[i] = false;
					ResetTimer();
				}
			}
		}

	}

	bool CheckIfReady()
	{
		for (int i = 0; i < 4; i++)
		{
			if (!choosen[i])
			{
				return false;
			}
		}			

		return true;
	}

	void ResetTimer()
	{
		counter = 0;
		for(int i = 0; i < 4; i ++)
			SetText("");
	}

	void DoTimer()
	{
		counter += Time.deltaTime;
		if (counter > 0f && counter < 1f)
			SetText("3");
		else if (counter > 1f && counter < 2f)
			SetText("2");
		else if (counter > 2f && counter < 3f)
			SetText("1");
		else if (counter > 3f)
			Application.LoadLevel("CharacterSelect");
	}

	void SetText(string text)
	{
		for(int i = 0; i < 4; i++)
			timerText[i].text = text;
	}
}
