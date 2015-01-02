using UnityEngine;
using System.Collections;

public class LevelMarker : MonoBehaviour {

	public float speed=1;

	public Transform levels;
	public Transform[] LevelPositions;

	static int LevelTracker;

	public int currentLevel;

	// Use this for initialization
	void Start () {

		currentLevel = GetLevelTracker ();
		LevelPositions = levels.GetComponentsInChildren<Transform> ();

		transform.position = LevelPositions [currentLevel].position;


		Debug.Log (LevelTracker);

	}
	
	// Update is called once per frame
	void Update () {



		transform.position=  Vector3.MoveTowards (transform.position, LevelPositions [currentLevel+1].position, speed);


	}

	void OnMouseDown(){

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
