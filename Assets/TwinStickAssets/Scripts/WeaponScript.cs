using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

[HideInInspector]
public Transform actorTransform;

[HideInInspector]
public WeaponController weaponController;
 

public float holdHeight = 0.5f;
public float holdSide = 0.5f;
public float fireRate = 0.1f;

public bool automaticFire = false;

[HideInInspector]
public bool triggerReleased = true;

[HideInInspector]
public float shootTimer = 0.0f;

public GameObject bullet ;
public GameObject bulletSpawn;

public int bulletCount = 1; //number of bullets shot at once
public int extraBulletCount = 0; //max num of extra bullets allowed
public float bulletSpread = 0.0f; //spread arc of multiple shots

public float recoilRecoverSpeed = 10.0f; //amount arc decreases per second when not shooting
public float recoilAmount = 0.5f; //amount arc increases per shot
public float maxRecoil = 10.0f;   //max recoil arc in degrees

[HideInInspector]
public float currentRecoilArc = 0.0f; //arc size in degrees
[HideInInspector]
public float currentRecoilAngle = 0.0f; //last shot angle

public GameObject bulletSound;
public GameObject muzzleFlash ;

public float gunbobAmountX = 0.5f;
public float gunbobAmountY = 0.5f;
public float currentGunbobX;
public float currentGunbobY;

 

void Awake ()
{

}
 

void Update()
{
	if(shootTimer > 0.0f)
	{
		shootTimer -= Time.deltaTime;
	}

	bool canRecoverRecoil = (automaticFire && triggerReleased) || !automaticFire;

	if(canRecoverRecoil && currentRecoilArc > 0)
	{
		currentRecoilArc -= recoilRecoverSpeed * Time.deltaTime;
		currentRecoilArc = Mathf.Max(0, currentRecoilArc);
	}

	if(canRecoverRecoil && currentRecoilAngle > 0)
	{
		currentRecoilAngle -= recoilRecoverSpeed * Time.deltaTime;
		currentRecoilAngle = Mathf.Max(0, currentRecoilAngle);
	}
	else if(canRecoverRecoil && currentRecoilAngle < 0)
	{
		currentRecoilAngle += recoilRecoverSpeed * Time.deltaTime;
		currentRecoilAngle = Mathf.Min(0, currentRecoilAngle);
	}

	if(weaponController != null)
	{
		rigidbody.useGravity = false;
		GetComponent<Collider>().enabled = false;

		float angle = (weaponController.transform.rotation.eulerAngles.y + currentRecoilAngle) * Mathf.PI / 180.0f;
		transform.position = new Vector3(weaponController.transform.position.x + (2.0f) * Mathf.Sin(angle), 
										 weaponController.transform.position.y, 
										 weaponController.transform.position.z + (2.0f) * Mathf.Cos(angle));
		transform.rotation = Quaternion.Euler(0, angle / Mathf.PI * 180.0f, 0);
	}
	else
	{
		rigidbody.useGravity = true;
		GetComponent<Collider>().enabled = true;
	}

}

public void fire()
{
    if (shootTimer <= 0 && (automaticFire || (!automaticFire && triggerReleased)))
    {

        currentRecoilAngle = Random.Range(-currentRecoilArc/2.0f, currentRecoilArc/2.0f);
    	if(bullet)
    	{
    		float angle = (transform.rotation.eulerAngles.y + currentRecoilAngle) * Mathf.PI / 180.0f;

    		int totalBullets = bulletCount + Random.Range(0, extraBulletCount);
    		for(int i = 0; i < totalBullets; i++)
    		{
    			float bulletAngle = angle + (Random.Range(-bulletSpread/2.0f, bulletSpread /2.0f) * Mathf.PI / 180.0f);

				GameObject obj = (GameObject) Instantiate(bullet, 
				new Vector3(transform.position.x, transform.position.y, transform.position.z ),  Quaternion.Euler(0, bulletAngle / Mathf.PI * 180.0f, 0));
				//obj.rigidbody.velocity = transform.forward * 60.0f;
			}
    	}

    	AudioSource bulletSource = GetComponent<AudioSource>();
    	if(bulletSource != null)
    	{
    		bulletSource.Play();
    	}
    	/*
        if (bullet)
            Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);

        if (bulletSound)
            holdSound = Instantiate(bulletSound, bulletSpawn.transform.position, bulletSpawn.transform.rotation);

        if (muzzleFlash)
            holdMuzzleFlash = Instantiate(muzzleFlash, bulletSpawn.transform.position, bulletSpawn.transform.rotation);

        currentRecoilZPos -= recoilAmount;
	*/
        shootTimer = fireRate;
        triggerReleased = false;
        currentRecoilArc += recoilAmount;
        currentRecoilArc = Mathf.Min(maxRecoil, currentRecoilArc);

    }
}

public void releaseTrigger()
{
	triggerReleased = true;
}

}
