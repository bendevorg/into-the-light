using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	float lifetime;
	Color color;
	Renderer renderer;
	int points;
	public float healthPlus;

	void Start(){
		renderer = GetComponent<Renderer>();
		color = renderer.material.color;
		StartCoroutine(DestroyPickup());
	}

	public void Lifetime(float _lifetime){

		lifetime = _lifetime;

	}

	public void Points(int _points){
		points = _points;
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

		Destroy(this.gameObject);

	}

	void OnTriggerEnter(Collider collider){
		
		if (collider.GetComponent<LivingEntity>() != null){
			collider.GetComponent<LivingEntity>().AddHealth(healthPlus);
		}
		GameController.gameController.AddPoint(points);
		Destroy(this.gameObject);

	}

}
