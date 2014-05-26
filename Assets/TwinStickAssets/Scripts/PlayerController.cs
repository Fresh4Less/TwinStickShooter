using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject playerPrefab;
	public Camera playerCamera;

	public int player = 1;
	private BuildMenu buildMenu;
	private WeaponController weaponController;
	private ActorController actorController;
	private GameObject playerCharacter;
	private TwinStickInputController inputController;
	bool isAlive = false;

	// Use this for initialization
	void Start () 
	{
		buildMenu = GetComponent<BuildMenu>();
		spawnPlayer(new Vector2(transform.position.x, transform.position.z));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(actorController.health < 0)
			isAlive = false;
		//if player is alive these are their actions
		if(isAlive)
		{
			//input
			inputController.setInputs(Input.GetAxis("LeftStickHorizontal" + player),
									  Input.GetAxis("LeftStickVertical" + player),
									  Input.GetAxis("RightStickHorizontal" + player),
									  Input.GetAxis("RightStickVertical" + player));

			if(Input.GetAxis("RightTrigger" + player) > 0.3)
			{
				weaponController.fire();
			}

			if(Input.GetAxis("RightTrigger" + player) < 0.3)
			{
				weaponController.releaseTrigger();
			}

			if(Input.GetButtonDown("PickUp" + player))
			{
				weaponController.pickUpWeapon();
			}

			if(Input.GetButtonDown("BuildMenu" + player))
			{
				buildMenu.enable();
			}
			if(Input.GetButtonUp("BuildMenu" + player))
			{
				buildMenu.disable();
			}

			if(buildMenu.isEnabled)
			{
				buildMenu.setInputs(Input.GetAxis("RightStickHorizontal" + player),
									Input.GetAxis("RightStickVertical" + player));
			}
			if(player==1)
				playerCamera.GetComponent<SmoothFollow>().target = playerCharacter.transform;
		}
		else
		{
			//if not alive respawn immediately for debug
			spawnPlayer(new Vector2(transform.position.x, transform.position.z));
		}

	}


	void spawnPlayer(Vector2 position)
	{

		isAlive = true;
		playerCharacter = (GameObject) Instantiate(playerPrefab, new Vector3(position.x, 2.0f, position.y), Quaternion.identity);
		weaponController = playerCharacter.GetComponent<WeaponController>();
		actorController = playerCharacter.GetComponent<ActorController>();
		inputController = playerCharacter.GetComponent<TwinStickInputController>();
		inputController.useExternalInput = true;

		buildMenu.followObject = playerCharacter.transform;
	}
}
