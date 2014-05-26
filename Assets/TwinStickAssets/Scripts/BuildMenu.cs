using UnityEngine;
using System.Collections;

public class BuildMenu : RadialMenu {

	// Use this for initialization
	protected override  void Start () {
		base.Start();
		createMenuItems(6);
		disable();
	
	}
	
	// Update is called once per frame
	protected override  void Update () {
		base.Update();
	}


	protected override void activateMenuItem(int index)
	{
		//Debug.Log("Selected " + index);
	}
}
