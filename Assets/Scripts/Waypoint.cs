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

	//public WaypointDirection waypointDirection;
	bool allowNorth;
	bool allowEast;
	bool allowSouth;
	bool allowWest;
	Transform teleportTarget;
	BoxCollider2D wayPointColl;

	void Start () {
	}
	public void SetVariables(bool _allowNorth, bool _allowEast, bool _allowSouth, bool _allowWest, Transform _teleportTarget){
		allowNorth = _allowNorth;
		allowEast = _allowEast;
		allowSouth = _allowSouth;
		allowWest = _allowWest;
		if (_teleportTarget != null)
			teleportTarget = _teleportTarget;
	}	

	public void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.GetComponent<Enemy>() != null){
			Enemy otherScript = other.GetComponent<Enemy>();

			bool repeat = true;
			while(repeat){
				int directionTemp = Random.Range(1, 5);
				if(directionTemp == 1 && allowNorth){
					otherScript.direction = Vector3.up;
					repeat = false;
				}
				if(directionTemp == 2 && allowEast){
					otherScript.direction = Vector3.right;
					repeat = false;
				}
				if(directionTemp == 3 && allowSouth){
					otherScript.direction = Vector3.down;
					repeat = false;
				}
				if(directionTemp == 4 && allowWest){
					otherScript.direction = Vector3.left;
					repeat = false;
				}
			}

			//Teleport
			if(teleportTarget != null){
				other.transform.position = teleportTarget.position;
			}
		}
	}

}
