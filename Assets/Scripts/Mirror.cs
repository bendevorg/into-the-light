﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class Mirror : ReflectEntity {

	public Light mirrorLight;

	void Awake(){
		if(GetComponent<Light>() != null){
			mirrorLight = GetComponent<Light>();
		}
	}

	public void Reflect(bool reflectStatus){
		print(transform.name);
		mirrorLight.enabled = reflectStatus;
	}


}
