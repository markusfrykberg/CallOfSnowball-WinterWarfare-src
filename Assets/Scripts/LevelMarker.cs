﻿using UnityEngine;
using System.Collections;

public class LevelMarker : MonoBehaviour {

	public float speed=1;

	public Transform levels;
	public Transform[] LevelPositions;

	static int LevelTracker;

	public int currentLevel;

	// Use this for initialization
	void Start () {
//		LevelTracker = 0;
//		SetLevelTracker ();
		currentLevel = GetLevelTracker ();
		LevelPositions = levels.GetComponentsInChildren<Transform> ();
//		GetLevelTracker ();
		transform.position = LevelPositions [currentLevel].position;
//		Debug.Log (LevelTracker);
//		nextLevel = LevelTracker + 1;
//		LevelTracker= nextLevel;
//		SetLevelTracker ();

		Debug.Log (LevelTracker);

	}
	
	// Update is called once per frame
	void Update () {



		transform.position=  Vector3.MoveTowards (transform.position, LevelPositions [currentLevel+1].position, speed);


	}

	void OnMouseDown(){
//		currentLevel++;
//		SetLevelTracker (currentLevel);
		Debug.Log ("click ");
		if (currentLevel >= 30)
						currentLevel = 0;
		Application.LoadLevel (currentLevel+1);
	}                   

	public void SetLevelTracker(int level){
		PlayerPrefs.SetInt ("LevelTracker", level);
		Debug.Log ("SetLevel: " +level);
		}
		
	public int GetLevelTracker(){
			LevelTracker = PlayerPrefs.GetInt ("LevelTracker");
			Debug.Log ("getLevel: " +LevelTracker);
			return LevelTracker;
			
		}

	void OnCollisionEnter(Collision other){

		}


}
