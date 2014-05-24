using UnityEngine;
using System.Collections;

public class ActorController : MonoBehaviour {

	public int health = 100;

	public GameObject bloodParticles;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void doDamage(int amount)
	{
		health -= amount;
		if(bloodParticles)
			Instantiate(bloodParticles, transform.position, transform.rotation);
		if(health < 0)
		Destroy(gameObject);
	}

	void OnControllerColliderHit(ControllerColliderHit hit) 
	{
   		if (hit.gameObject.GetComponent<WeaponScript>() != null)
   		{
    		Physics.IgnoreCollision(hit.collider, collider);
    	}
	}
}
