using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterMotor))]
[AddComponentMenu ("Character/TwinStick Input Controller")]

public class TwinStickInputController : MonoBehaviour {

	public bool useExternalInput = false;
	public int player;
	private CharacterMotor motor;
	private float lh;
	private float lv;
	private float rh;
	private float rv;

	// Use this for initialization
	void Start () {
	
	}

	void Awake() {
		motor = GetComponent<CharacterMotor>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!useExternalInput)
		{
			lh = Input.GetAxis("LeftStickHorizontal" + player);
		    lv = Input.GetAxis("LeftStickVertical" + player);
		    rh = Input.GetAxis("RightStickHorizontal" + player);
		    rv = Input.GetAxis("RightStickVertical" + player);
		}

		Vector3 moveDirectionVector = new Vector3(lh, 0, -lv);
	
		if (moveDirectionVector != Vector3.zero) {
			// Get the length of the directon vector and then normalize it
			// Dividing by the length is cheaper than normalizing when we already have the length anyway
			float directionLength = moveDirectionVector.magnitude;
			moveDirectionVector = moveDirectionVector / directionLength;
			
			// Make sure the length is no bigger than 1
			directionLength = Mathf.Min(1, directionLength);
			
			// Make the input vector more sensitive towards the extremes and less sensitive in the middle
			// This makes it easier to control slow speeds when using analog sticks
			directionLength = directionLength * directionLength;
			
			// Multiply the normalized direction vector by the modified length
			moveDirectionVector = moveDirectionVector * directionLength;
		}
		
		Vector3 lookDirectionVector = new Vector3(rh, 0, -rv);
		if (lookDirectionVector != Vector3.zero) {
			// Get the length of the directon vector and then normalize it
			// Dividing by the length is cheaper than normalizing when we already have the length anyway
			float directionLength = lookDirectionVector.magnitude;
			lookDirectionVector = lookDirectionVector / directionLength;
			
			// Make sure the length is no bigger than 1
			directionLength = Mathf.Min(1, directionLength);
			
			// Make the input vector more sensitive towards the extremes and less sensitive in the middle
			// This makes it easier to control slow speeds when using analog sticks
			directionLength = directionLength * directionLength;
			
			// Multiply the normalized direction vector by the modified length
			lookDirectionVector = lookDirectionVector * directionLength;
		}

		// Apply the direction to the CharacterMotor
		motor.inputMoveDirection = moveDirectionVector;
		if(rh != 0.0f || rv != 0.0f)
			transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(lookDirectionVector.x,lookDirectionVector.z) * 180.0f / Mathf.PI, Vector3.up);
		//Debug.Log(moveDirectionVector);
		//Debug.Log(lookDirectionVector);
		//motor.inputJump = Input.GetButton("Jump");

		//jump
		if(!useExternalInput)
			motor.inputJump = Input.GetButton("Jump" + player);
	}

	void OnGUI()
	{
		GUI.Label(new Rect(0,0,Screen.width,Screen.height), motor.inputMoveDirection.ToString());
		GUI.Label(new Rect(0,50,Screen.width,Screen.height), "" + transform.rotation.eulerAngles.y);
	}

	public void setInputs(float leftHorizontal, float leftVertical, float rightHorizontal, float rightVertical)
	{
		lh = leftHorizontal;
		lv = leftVertical;
		rh = rightHorizontal;
		rv = rightVertical;
	}
}
