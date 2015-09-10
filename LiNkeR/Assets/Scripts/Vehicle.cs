using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Vehicle : MonoBehaviour {

	public enum SittingDirection
	{
		BOTTOM,
		TOP,
		LEFT,
		RIGHT
	}
	public SittingDirection sittingDir;

	[SerializeField] public int playerID = 0;
	public GameObject linkedCar;
	[SerializeField] float health = 100f;
	[SerializeField] float speed;
	[SerializeField] float maxAcceleration;
	[SerializeField] float minAcceleration;
	[SerializeField] float accelerationSpeed = 0.02f;
	[SerializeField] float slowDownMultiplyer = 0.8f;
	[SerializeField] float bounciness = 100f;

	[SerializeField] AudioSource engineSound;
	[SerializeField] public float minEnginePitch;
	[SerializeField] public float maxEnginePitch;

	[SerializeField] public TextMesh positionText;
	[SerializeField] AudioClip collisionSound;

	public float boostSpeed = 1.0f;
	public float boostTimer = 1.0f;
	public float boostCounter = 0.0f;

	public float terrainMultiplyer = 1.0f;

	bool isAccelerating = false;
	float acceleration;

	public int currentPosition;
	public int checkPointID = 0;
	public int lap = 0;
	public bool controlsEnabled = true;

	public GameObject item;
	[SerializeField] GameObject itemInstPoint;

    public Character playerCharacter;

	Vector2 inputDirection;

	Rigidbody2D rigid;

	GamePadState prevState;
	GamePadState currState;

	void Start()
	{
		rigid = GetComponent<Rigidbody2D>();
		//GameManager.inst.SetCharacterIcons(playerID);
	}

	void FixedUpdate ()
	{
		if(lap == 3)
			controlsEnabled = false;

		SetInputStates();

		CalculateAcceleration();

		CalculateBoosts();

		SetRotation();

		SetEngineSoundVariables();

		CheckForPlacingItem();

		ApplyForces();
	}

	void SetInputStates()
	{
		prevState = currState;
		currState = GamePad.GetState((PlayerIndex)playerID);
		if (!currState.IsConnected)
		{
			//print("Invalid Controller: " + playerID);
			return;
		}

		switch(sittingDir)
		{
		case SittingDirection.BOTTOM:
			inputDirection = new Vector2(currState.ThumbSticks.Left.X, currState.ThumbSticks.Left.Y);
			break;
		case SittingDirection.TOP:
			inputDirection = new Vector2(-currState.ThumbSticks.Left.X, -currState.ThumbSticks.Left.Y);
			break;
		case SittingDirection.LEFT:
			inputDirection = new Vector2(currState.ThumbSticks.Left.Y, -currState.ThumbSticks.Left.X);
			break;
		case SittingDirection.RIGHT:
			inputDirection = new Vector2(-currState.ThumbSticks.Left.Y, currState.ThumbSticks.Left.X);
			break;
		}

	}

	void CalculateAcceleration()
	{
		if (inputDirection.magnitude != 0)
		{
			acceleration += inputDirection.magnitude * accelerationSpeed;
			isAccelerating = true;
		}
		else
		{
			acceleration -= 0.02f;
			isAccelerating = false;
		}

		if (acceleration > maxAcceleration)
			acceleration = maxAcceleration;
		else if (acceleration < minAcceleration)
			acceleration = minAcceleration;
	}

	void CalculateBoosts()
	{
		boostCounter += Time.deltaTime;
		if(boostCounter >= boostTimer)
		{
			boostSpeed = 1.0f;
		}
	}

	void SetRotation()
	{
		float angle = Mathf.Atan2(-inputDirection.y, -inputDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void ApplyForces()
	{
		if(controlsEnabled)
		{
			rigid.AddForce(inputDirection * speed * acceleration * terrainMultiplyer * boostSpeed);
			
			if (!isAccelerating)
			{
				rigid.AddForce(-rigid.velocity * slowDownMultiplyer);
			}
		}
	}

	public void SetEngineSoundVariables()
	{
		float accRange = maxAcceleration - minAcceleration;
		float acc1percent = accRange / 100;
		float pitchRange = maxEnginePitch - minEnginePitch;

		float pitch1percent = pitchRange / 100;

		float accPercent = acceleration / acc1percent;

		engineSound.pitch = minEnginePitch + (accPercent * pitch1percent);

	}

	public void CheckForPlacingItem()
	{
		if(item != null)
		{
			if(prevState.Triggers.Right < 0.5f && currState.Triggers.Right > 0.5f)
			{
				GameObject tempO = (GameObject)Instantiate(item, itemInstPoint.transform.position, Quaternion.identity);
				tempO.SendMessage("SetOwner", gameObject);
                GameManager.inst.SetDefaultIcon(playerID);
				item = null;
			}
		}
	}

	public void TakeHealth(float damage)
	{
		health -= damage;
	}

	public void GiveHealth(float heal)
	{
		health += heal;
	}

	public void SetTerrainModifier(float value)
	{
		terrainMultiplyer = value;
	}

	public void SetBoostSpeed(float boost, float timer)
	{
		boostSpeed = boost;
		boostTimer = timer;
		boostCounter = 0.0f;
	}

	public void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Player" || col.gameObject.tag == "Wall")
		{
			Vector2 vecDiff = transform.position - col.transform.position;
			rigid.AddForce(vecDiff * bounciness);
		}
		else if(col.gameObject.tag == "Bullet")
		{
			health -= 1;
			Destroy(col.gameObject);
		}

		AudioSource.PlayClipAtPoint(collisionSound, Vector2.zero);
	}
}
