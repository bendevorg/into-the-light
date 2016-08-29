using UnityEngine;
using System.Collections;

public class MirrorController : MonoBehaviour {

	public Mirror playerMirror;

	[HideInInspector] public Mirror currentMirror;

	void Start(){

		if (playerMirror != null){
			currentMirror = Instantiate(playerMirror, transform.position, Quaternion.identity) as Mirror;
			currentMirror.transform.parent = transform;
			SetLight(false);
		}

	}

	public void SetLight(bool lightStatus){

		currentMirror.Reflect(lightStatus);

	}
}
