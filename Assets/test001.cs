using UnityEngine;
using System.Collections;

public class test001 : MonoBehaviour {

	public float speed;
	

	void Update () {
		if(Input.GetKey (KeyCode.UpArrow))
			transform.position +=new Vector3(0, speed * Time.deltaTime, 0);

		if(Input.GetKey (KeyCode.DownArrow))
			transform.position +=new Vector3(0, -speed * Time.deltaTime, 0);
	
	}
}
