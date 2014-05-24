using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	
	public int damage = 10;

	float maxDist  = 100000.0f;

	//GameObject decalHitWall;

	//float floatInFrontOfWall = 0.00001f;

	float fadeDuration = 0.25f;
	float fadeTimer = 0.0f;

	void Start()
	{
		 RaycastHit hit;

		 int layerMask = ~(1<<9); //raycast ignore items

	    if (Physics.Raycast(transform.position, transform.forward, out hit, maxDist, layerMask))

	    {

	        //if (decalHitWall && hit.transform.tag == "Level Parts")

	       //     Instantiate(decalHitWall, hit.point + (hit.normal * floatInFrontOfWall), Quaternion.LookRotation(hit.normal));
	    	ActorController other = hit.transform.GetComponent<ActorController>();
			if(other)
			{
				other.doDamage(damage);	
			}

		    LineRenderer lineRenderer = GetComponent<LineRenderer>();
		    lineRenderer.SetVertexCount(2);
		    lineRenderer.SetPosition(0, transform.position);
		    lineRenderer.SetPosition(1, hit.point);
	    }

	}

	void Update () 

	{
		fadeTimer += Time.deltaTime;
		fadeTimer = Mathf.Min(fadeDuration, fadeTimer);
		GetComponent<LineRenderer>().material.SetColor("_TintColor", new Color(.5f,.5f,.5f, 0.5f - (fadeTimer / fadeDuration / 2.0f)));
		//GetComponent<LineRenderer>().material.color = Color.Lerp(Color.clear, Color.white, 1.0f - (fadeTimer / fadeDuration));
		//Debug.Log(1.0f - (fadeTimer / fadeDuration));
		if(fadeTimer >= fadeDuration)
			Destroy(gameObject);

	}

/*
	void OnCollisionEnter(Collision collision)
	{
		ActorController other = collision.gameObject.GetComponent<ActorController>();
		if(other)
		{
			other.doDamage(damage);
			
		}
		Destroy(gameObject);
	}
	*/
}
