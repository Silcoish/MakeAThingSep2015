using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Gun : MonoBehaviour {

	[SerializeField] Vehicle parentVehicleScript;
	[SerializeField] GameObject instantiatePoint;
	[SerializeField] GameObject bullet;
	[SerializeField] float cooldown = 0.2f;
	float counter = 0.0f;
	Vector2 inputDirection;
	GamePadState prevState;
	GamePadState currState;

	void Update()
	{
		counter += Time.deltaTime;
	}

	void FixedUpdate () {
		prevState = currState;
		currState = GamePad.GetState((PlayerIndex)parentVehicleScript.playerID);
		if(!currState.IsConnected)
		{
			return;
		}
		inputDirection = new Vector2(currState.ThumbSticks.Right.X, currState.ThumbSticks.Right.Y);
		float angle = Mathf.Atan2(-inputDirection.y, -inputDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		if(currState.Triggers.Right >= 0.5f && counter >= cooldown)
		{
			counter = 0;
			GameObject bul = (GameObject)Instantiate(bullet, instantiatePoint.transform.position, transform.rotation);
			bul.GetComponent<Rigidbody2D>().AddForce((Vector2)bul.transform.right * - 100f);
		}
	}
}
