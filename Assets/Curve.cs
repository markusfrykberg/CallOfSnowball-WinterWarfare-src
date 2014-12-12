using UnityEngine;
using System.Collections;

public class Curve : MonoBehaviour {
	[SerializeField]
	bool allowNorth = true;
	[SerializeField]
	bool allowEast = true;
	[SerializeField]
	bool allowSouth = true;
	[SerializeField]
	bool allowWest = true;
	public Transform teleportTarget;
	Waypoint wayPoint;

	void Start () {
		wayPoint = GetComponentInChildren<Waypoint> ();
		wayPoint.SetVariables (allowNorth, allowEast, allowSouth, allowWest, teleportTarget);
	}
	
	void Update () {
	
	}
}
