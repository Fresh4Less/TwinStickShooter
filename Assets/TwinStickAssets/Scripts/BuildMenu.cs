using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildMenu : MonoBehaviour {

	public Texture2D menuTexture;

	public bool isEnabled = false;

	public Transform followObject;
	private GameObject menuBackground;
	private List<GameObject> menuItems;
	private int selectedMenuItem = -1;

	private float rh;
	private float rv;

	// Use this for initialization
	void Start () {

		menuItems = new List<GameObject>();

		menuBackground = createMenuItem(360.0f, 0.8f, 2.2f, 0.0f, true);

		for(int i = 0; i < 5; i++)
		{
			menuItems.Add(createMenuItem(360.0f / 5.0f, 1.0f, 2.0f, i*(360.0f / 5.0f) + 90.0f, false));
		}
		disable();
	}
	
	// Update is called once per frame
	void Update () {
		//face the camera
    	//transform.up = Camera.main.transform.position - transform.position;
    	//transform.forward = -Camera.main.transform.up;
		if(followObject)
		{
    		menuBackground.transform.position = new Vector3(followObject.position.x, 1.0f, followObject.position.z);
    		foreach(GameObject item in menuItems)
    		{
				item.transform.position = new Vector3(followObject.position.x, 1.01f, followObject.position.z);
    		}
		}

		//selection
		if(rh > 0.1 || rh < -0.1 || rv > 0.1 || rv < -0.1)
		{
			float angle = Mathf.Atan2(rh, rv) * 180.0f / Mathf.PI;
			while(angle < 0.0f)
				angle += 360.0f;
			if(angle < 0.1f)
				angle += 1.0f;
			selectMenuItem((int)((360.0f - angle) / 360.0f * menuItems.Count));
		}
		else
		{
			selectMenuItem(-1);
		}
}

	public void enable()
	{
		isEnabled = true;
		menuBackground.GetComponent<MeshRenderer>().enabled = true;
		foreach(GameObject item in menuItems)
		{
			item.GetComponent<MeshRenderer>().enabled = true;
		}
		
	}

	public void disable()
	{
		isEnabled = false;
		menuBackground.GetComponent<MeshRenderer>().enabled = false;
		foreach(GameObject item in menuItems)
		{
			item.GetComponent<MeshRenderer>().enabled = false;
		}
	}

	void selectMenuItem(int index)
	{
		if(selectedMenuItem != -1)
		{		
			menuItems[selectedMenuItem].GetComponent<MeshRenderer>().material.color = new Color(0.0f,0.4f,0.5f, 1.0f);
		}
		if(index != -1)
		{
			menuItems[index].GetComponent<MeshRenderer>().material.color = Color.white;
		}
		selectedMenuItem = index;
	}

	Mesh CreatePlaneMesh(float width, float height)
	{
		Mesh m = new Mesh();
		m.name = "ScriptedMesh";
		m.vertices = new Vector3[] {
		   new Vector3(-width, -height, 0.01f),
		   new Vector3(width, -height, 0.01f),
		   new Vector3(width, height, 0.01f),
		   new Vector3(-width, height, 0.01f)
		};
		m.uv = new Vector2[] {
		   new Vector2 (0, 0),
		   new Vector2 (0, 1),
		   new Vector2(1, 1),
		   new Vector2 (1, 0)
		};
		m.triangles = new int[] { 0, 1, 2, 0, 2, 3};
		m.RecalculateNormals();
		
		return m;
	}


	GameObject createMenuItem(float arcAngle, float innerRadius, float outerRadius, float offsetAngle, bool isBackground)
	{
		//create the gameobject
		GameObject menuItem = new GameObject("MenuPlane");
		MeshFilter meshFilter = (MeshFilter) menuItem.AddComponent(typeof(MeshFilter));
		//meshFilter.mesh = CreatePlaneMesh(3.0f, 3.0f);
		meshFilter.mesh = CreateRingSegmentMesh(arcAngle, innerRadius, outerRadius);
		MeshRenderer renderer = menuItem.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		renderer.material.shader = Shader.Find("Custom/BuildMenuShader");
		//Texture2D tex = new Texture2D(1, 1);
		//tex.SetPixel(0, 0, Color.green);
		//tex.Apply();
		//renderer.material.mainTexture = tex;
		renderer.material.mainTexture = menuTexture;
		if(isBackground)
		{
			renderer.material.color = new Color(0.0f,0.4f,0.5f, 1.0f);
			renderer.material.renderQueue = 4000;
		}
		else
		{
			renderer.material.color = new Color(0.0f,0.5f,0.7f,1.0f);
			renderer.material.renderQueue = 4001;
		}
		menuItem.transform.rotation = Quaternion.Euler(-90.0f, offsetAngle, 0.0f);
		return menuItem;
	}

	Mesh CreateRingSegmentMesh(float angle, float innerRadius, float outerRadius)
	{
		Mesh m = new Mesh();
		m.name = "RingSegmentMesh";
		List<Vector3> vertices = new List<Vector3>();
		List<Vector2> uvs = new List<Vector2>();
		//number of vertices per each side of ring
		int vertexCount = (int) Mathf.Max(40.0f * (angle / 360.0f), 2.0f);
		for(int i = 0; i <= vertexCount; i++)
		{
			float currentAngle = i * (angle / vertexCount);
			float currentAngleRad = currentAngle * (Mathf.PI / 180.0f);
			
			float xMult = Mathf.Cos(currentAngleRad);
			float yMult = Mathf.Sin(currentAngleRad);
			vertices.Add(new Vector3(xMult * innerRadius, yMult * innerRadius, 0.01f));
			vertices.Add(new Vector3(xMult * outerRadius, yMult * outerRadius, 0.01f));
			uvs.Add(new Vector2(currentAngle/angle,0));
			uvs.Add(new Vector2(currentAngle/angle,1));

		}
		m.vertices = vertices.ToArray();
		m.uv = uvs.ToArray();
		/*
		m.uv = new Vector2[] {
		   new Vector2 (0, 0),
		   new Vector2 (0, 1),
		   new Vector2(1, 1),
		   new Vector2 (1, 0)
		};
		*/

		List<int> triangles = new List<int>();
		for(int i = 0; i < vertexCount; i++)
		{
			int offset = i;
			triangles.Add(0 + offset*2);
			triangles.Add(1 + offset*2);
			triangles.Add(2 + offset*2);
			triangles.Add(2 + offset*2);
			triangles.Add(1 + offset*2);
			triangles.Add(3 + offset*2);
		}
		m.triangles = triangles.ToArray();
		//m.triangles = new int[] { 0, 1, 2, 0, 2, 3};
		m.RecalculateNormals();
		
		return m;
	}

	public void setInputs(float rightHorizontal, float rightVertical)
	{
		rh = rightHorizontal;
		rv = rightVertical;
	}
}
