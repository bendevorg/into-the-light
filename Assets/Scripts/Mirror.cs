using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class Mirror : MonoBehaviour {
	[HideInInspector]
	public Light mirrorLight;

	SoundManager soundManager;

	void Awake(){
		if(GetComponent<Light>() != null){
			mirrorLight = GetComponent<Light>();
		}
		soundManager = GetComponent<SoundManager>();
	}

	public void Reflect(bool reflectStatus){
		mirrorLight.enabled = reflectStatus;
		if (!reflectStatus){
			soundManager.Stop();
		}
	}


}
