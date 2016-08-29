using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	float lifetime;
	Color color;
	Renderer renderer;

	void Start(){
		renderer = GetComponent<Renderer>();
		color = renderer.material.color;
		StartCoroutine(DestroyPickup());
	}

	public void Lifetime(float _lifetime){

		lifetime = _lifetime;

	}

	IEnumerator DestroyPickup(){

		float currentTime = 0f;

		while(color.a > 0.1f){

			color = renderer.material.color;
			color.a = Mathf.Lerp(color.a, 0, 0.03f / (lifetime/2));
			renderer.material.color = color;

			currentTime += Time.time;

			yield return null;

		}

		print(currentTime);

	}

}
