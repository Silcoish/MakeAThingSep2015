using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Vehicle : MonoBehaviour {

	[SerializeField] public int playerID = 0;
	[SerializeField] float speed;
	[SerializeField] float maxAcceleration;
	[SerializeField] float minAcceleration;
	[SerializeField] float accelerationSpeed = 0.02f;
	[SerializeField] float slowDownMultiplyer = 0.8f;
	bool isAccelerating = false;
	float acceleration;
	Vector2 inputDirection;
	Rigidbody2D rigid;

	GamePadState prevState;
	GamePadState currState;

	void Start()
	{
		rigid = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate ()
	{
		prevState = currState;
		currState = GamePad.GetState((PlayerIndex)playerID);
		if (!currState.IsConnected)
		{
			print ("Invalid Controller: " + playerID);
			return;
		}


	//	print (currState.ThumbSticks.Left.X);
		inputDirection = new Vector2(currState.ThumbSticks.Left.X, currState.ThumbSticks.Left.Y);

		//acceleration
		if(inputDirection.magnitude != 0)
		{
			acceleration += inputDirection.magnitude * accelerationSpeed;
			isAccelerating = true;
		}
		else
		{
			acceleration -= 0.02f;
			isAccelerating = false;
		}

		if(acceleration > maxAcceleration)
			acceleration = maxAcceleration;
		else if(acceleration < minAcceleration)
			acceleration = minAcceleration;

		float angle = Mathf.Atan2(-inputDirection.y, -inputDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		rigid.AddForce(inputDirection * speed * acceleration);

		if(!isAccelerating)
		{
			rigid.AddForce(-rigid.velocity * slowDownMultiplyer);
		}
	}
}
