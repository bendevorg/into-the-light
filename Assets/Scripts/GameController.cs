using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public static GameController gameController;
	public List<Transform> visibleTargets;

	public event System.Action FindTargets;

	public static GameController Game {
		get{
			return gameController;
		}
	}

	void Awake(){
		if(gameController != null && gameController != this){
			Destroy(this.gameObject);
		} else {
			gameController = this;
		}
	}

	void Start () {
		
		StartCoroutine("GetAllTargets", .2f);

	}

	IEnumerator GetAllTargets(float delay){

		while(true){
			yield return new WaitForSeconds (delay);
			visibleTargets = new List<Transform>();
			if(FindTargets != null){
				FindTargets();
			}
		}
	
	}
}
