using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeAndPlacement : MonoBehaviour {

	public float terrainPositionNorth;
	public float terrainPositionEast;
	public float terrainPositionSouth;
	public float terrainPositionWest;


	/*
	void GenerateTerrainData()
	{
		terrain.terrainData.heightmapResolution = resolution;
		terrain.terrainData.baseMapResolution = resolution;

		terrain.terrainData.size = new Vector3(length, height, width);
		terrain.terrainData.SetHeights(0, 0, GenerateHeights());
	}
	*/

	// Use this for initialization
	void Start () 
	{
		terrainPositionNorth = Terrain.activeTerrain.transform.localScale.z * 5f;
		//terrainSizeNorth = GetComponent<Terrain>().terrainData.size.x;
		terrainPositionEast = Terrain.activeTerrain.transform.localScale.x * 5f;
		terrainPositionSouth = Terrain.activeTerrain.transform.localScale.z * (-5f);
		terrainPositionWest = Terrain.activeTerrain.transform.localScale.x * (-5f);

		if (GetComponentInChildren<Object> ().name.Equals ("West Wall")) 
		{
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
