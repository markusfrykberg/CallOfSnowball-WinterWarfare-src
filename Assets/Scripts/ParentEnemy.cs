using UnityEngine;
using System.Collections;

public class ParentEnemy : Enemy
{




	void Awake(){
		healthbar = GameObject.Find ("BossHealth");
		healthbar.renderer.enabled = true;
	}

}

