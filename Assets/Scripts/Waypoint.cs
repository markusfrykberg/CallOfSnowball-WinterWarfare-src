using UnityEngine;
using System.Collections;

public enum WaypointDirection{
	north,
	east,
	south,
	west,
	random
}

public class Waypoint : MonoBehaviour {

	public WaypointDirection waypointDirection;
	public Transform TeleportTarget;


	void Start () {
	}
	
	void Update () {
	
	}
	public void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.GetComponent<Enemy>() != null){
			Enemy otherScript = other.GetComponent<Enemy>();

			//Change Direction
			if(waypointDirection == WaypointDirection.north)
				otherScript.direction = Vector3.up;
			if(waypointDirection == WaypointDirection.east)
				otherScript.direction = Vector3.right;
			if(waypointDirection == WaypointDirection.south)
				otherScript.direction = Vector3.down;
			if(waypointDirection == WaypointDirection.west)
				otherScript.direction = Vector3.left;

			//Random Direction
			if(waypointDirection == WaypointDirection.random){
				int directionTemp = Random.Range(1, 5);
				if(directionTemp == 1)
					otherScript.direction = Vector3.up;
				if(directionTemp == 2)
					otherScript.direction = Vector3.right;
				if(directionTemp == 3)
					otherScript.direction = Vector3.down;
				if(directionTemp == 4)
					otherScript.direction = Vector3.left;
			}

			//Teleport
			if(TeleportTarget != null){
				other.transform.position = TeleportTarget.position;
			}
		}
	}
}
