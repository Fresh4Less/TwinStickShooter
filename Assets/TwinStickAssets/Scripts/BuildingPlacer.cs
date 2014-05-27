using UnityEngine;
using System.Collections;

public class BuildingPlacer : MonoBehaviour {

	public Material buildingPlacedMaterial;

	public bool isActive = false;
	public GameObject currentBuilding;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(currentBuilding)
		{
			float currentAngle = transform.rotation.eulerAngles.y - 90.0f;
			float currentAngleRad = currentAngle * (Mathf.PI / 180.0f);
			
			float xMult = Mathf.Cos(currentAngleRad);
			float yMult = Mathf.Sin(currentAngleRad);

			currentBuilding.transform.position = new Vector3(transform.position.x + (2.0f * xMult), transform.position.y + 0.0f, transform.position.z + (-2.0f * yMult));
			currentBuilding.transform.rotation = transform.rotation;
		}

	}

	public void beginBuildingPlacement(GameObject building)
	{
		currentBuilding = (GameObject) Instantiate(building, new Vector3(0,0,0), Quaternion.identity);
		currentBuilding.GetComponent<Collider>().enabled = false;
		//building.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y + 0.0f, transform.position.z + 0.0f);
		//building.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.1f);
		isActive = true;
	}

	public void placeBuilding()
	{
		currentBuilding.renderer.material = buildingPlacedMaterial;
		currentBuilding.GetComponent<Collider>().enabled = true;
		currentBuilding = null;
		isActive = false;
	}

	public void cancelBuilding()
	{
		Destroy(currentBuilding);
		currentBuilding = null;
		isActive = false;
	}
}
