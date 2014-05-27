using UnityEngine;
using System.Collections;

public class BuildMenu : RadialMenu {

	public GameObject[] buildings;

	// Use this for initialization
	protected override  void Start () {
		base.Start();
		createMenuItems(buildings.Length);
		disable();
	
	}
	
	// Update is called once per frame
	protected override  void Update () {
		base.Update();
	}


	protected override void activateMenuItem(int index)
	{
		//Debug.Log("Selected " + index);
		//maybe decouple this
		gameObject.GetComponent<PlayerController>().beginBuildingPlacement(buildings[index]);
	}
}
