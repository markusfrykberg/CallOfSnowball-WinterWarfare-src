using UnityEngine;
using System.Collections;

public class LevelScriptTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown(){
		PlayerPrefs.SetInt ("LevelTracker", Application.loadedLevel);
		Application.LoadLevel ("WorldMap");
		}
}
