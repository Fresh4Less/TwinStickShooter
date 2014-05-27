using UnityEngine;
using System.Collections;

public class TwinStickCameraFollow : MonoBehaviour
{

		
	public GameObject target;
	public int damping = 5;

	
		// LateUpdate is called once per frame, after Update.
		void LateUpdate ()
		{
			if (!target) 
							return;
		float currentX = transform.position.x;
		float currentZ = transform.position.z;
		float desiredX = target.transform.position.x + 4 * Mathf.Cos ((Mathf.PI*(-target.transform.rotation.eulerAngles.y + 90.0f))/180);
		float desiredZ = target.transform.position.z + 4 * Mathf.Sin ((Mathf.PI*(-target.transform.rotation.eulerAngles.y + 90.0f))/180);
		currentX = Mathf.Lerp (currentX, desiredX, damping * Time.deltaTime);
		currentZ = Mathf.Lerp (currentZ, desiredZ, damping * Time.deltaTime);
		transform.position = new Vector3(currentX, 14.0f, currentZ);
		}
}