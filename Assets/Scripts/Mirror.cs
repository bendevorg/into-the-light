using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class Mirror : MonoBehaviour {
	[HideInInspector]
	public Light mirrorLight;

	AudioSource myAudioSource;

	void Awake(){
		if(GetComponent<Light>() != null){
			mirrorLight = GetComponent<Light>();
		}
	}

	public void Reflect(bool reflectStatus){
		mirrorLight.enabled = reflectStatus;
	}


}
