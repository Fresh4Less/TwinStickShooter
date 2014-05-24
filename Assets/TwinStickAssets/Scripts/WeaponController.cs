using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	private WeaponScript currentWeapon;

	WeaponScript currentGun;
	float pickupDistance = 2;
	float throwGunUpForce = 300;
	float throwGunForwardForce = 300;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void fire()
	{
		if(currentGun != null)
		{
			currentGun.fire();
		}
	}

	public void releaseTrigger()
	{
		if(currentGun != null)
		{
			currentGun.releaseTrigger();
		}
	}

	public void pickUpWeapon()
	{

		Collider[] colliders = Physics.OverlapSphere (transform.position, pickupDistance);
		WeaponScript nearestWeapon = null;
	    foreach(Collider hit in colliders) 
	    {
	        WeaponScript weaponComponent = hit.transform.gameObject.GetComponent<WeaponScript>();
	        if(weaponComponent != null && weaponComponent.weaponController == null)
	        {
	        	if(nearestWeapon == null || Vector3.Distance(weaponComponent.transform.position, transform.position) < Vector3.Distance(nearestWeapon.transform.position, transform.position))
	        	{
	        		nearestWeapon = weaponComponent;
	        	}
	        }
	    }

	    if(nearestWeapon != null)
	    {
	    	if(currentGun != null)
	    	{
	    		currentGun.weaponController = null;
	    		currentGun.rigidbody.AddRelativeForce(0, throwGunUpForce, throwGunForwardForce);
	    		currentGun.rigidbody.AddRelativeTorque(0, 0, 50);
	    	}

	    	currentGun = nearestWeapon;
	    	currentGun.weaponController = this;
	    }
	}
}
