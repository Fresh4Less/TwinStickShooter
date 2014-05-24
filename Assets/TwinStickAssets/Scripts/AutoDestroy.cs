using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	public float lifetime = 1.0f;

	private float timer = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= lifetime)
			Destroy(gameObject);
	}
}
