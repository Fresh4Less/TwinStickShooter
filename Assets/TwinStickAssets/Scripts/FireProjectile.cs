using UnityEngine;
using System.Collections;

public class FireProjectile : MonoBehaviour {

	public GameObject projectile;
	private int player;

	private CharacterController characterController;

	// Use this for initialization
	void Awake () {
		characterController = GetComponent<CharacterController>();
		player = GetComponent<PlayerController>().player;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("RightTrigger" + player) > 0.3)
		{
			float angle = transform.rotation.eulerAngles.y * Mathf.PI / 180.0f;
			GameObject obj = (GameObject) Instantiate(projectile, 
				new Vector3(transform.position.x + characterController.radius * Mathf.Sin(angle), transform.position.y, transform.position.z + characterController.radius * Mathf.Cos(angle)), Quaternion.identity);
			obj.rigidbody.velocity = transform.forward * 8.0f;
		}
	}
}
