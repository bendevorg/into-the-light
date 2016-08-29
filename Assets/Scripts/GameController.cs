using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public static GameController gameController = null;
	static int points;
	static int wave;
	static int enemiesRemaning;

	GameObject player;

	void Awake(){
		if(gameController != null && gameController != this){
			Destroy(this.gameObject);
		} else {
			gameController = this;
		}

	}

	public void Start(){
		
	}

	public void AddPoint(int _points){
		points += _points;
	}

	public void Reset(){

		player = GameObject.FindGameObjectWithTag("Player");

		if (player != null){
			if (player.GetComponent<MirrorController>() != null){
				if (player.GetComponent<MirrorController>().currentMirror != null){
					player.GetComponent<MirrorController>().SetLight(false);
				}
			}
		}

		//Spotlight[] spotlights = FindObjectsOfType<Spotlight>();
		//Pickup[] pickups = FindObjectsOfType<Pickup>();

		//foreach(Spotlight spotlight in spotlights){
			//Destroy(spotlight.gameObject);
		//}

		//foreach(Pickup pickup in pickups){
		//	Destroy(pickup.gameObject);
		//}

	}
}
