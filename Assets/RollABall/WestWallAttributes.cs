using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WestWallAttributes : MonoBehaviour {

	public float positionWest ;
	public GameObject Ground;
	// Use this for initialization
	void Start () {
		Ground = GameObject.Find ("Ground");
		positionWest = Ground.transform.localScale.x * (-5);
		//this.transform.position.x = positionWest;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
