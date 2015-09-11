using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class ReturnToMainMenu : MonoBehaviour {

    GamePadState[] prevState = new GamePadState[4];
    GamePadState[] currState = new GamePadState[4];

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            currState[i] = GamePad.GetState((PlayerIndex)i);
            prevState[i] = currState[i];
        }
        
    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            prevState[i] = currState[i];
            currState[i] = GamePad.GetState((PlayerIndex)i);
        }

        for(int j = 0; j < 4; j++)
        {
            if (prevState[j].Buttons.B == ButtonState.Pressed && currState[j].Buttons.B == ButtonState.Released)
                Application.LoadLevel("DirectionSelect");
        }
    }
}
