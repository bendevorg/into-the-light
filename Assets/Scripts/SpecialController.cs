using UnityEngine;
using System.Collections;

public class SpecialController : MonoBehaviour {

	public float specialMaxSize;
	public float specialSpeed;
	public float specialCooldown;
	public float power;
	PlayerController playerController;

	void Start () {
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

	}

	IEnumerator SpecialAttack(){
		float timer = 0;


		while(specialMaxSize > transform.localScale.x){

			// special.transform.localScale = Vector3.Lerp(special.transform.localScale, );
			transform.localScale += new Vector3(1, 1, 1) * Time.fixedDeltaTime * specialSpeed;
			timer += Time.fixedDeltaTime;
			yield return null;
		}

		playerController.canSpecial = true;
		Destroy(this.gameObject);
		
	}

	public void StartSpecial(){
		StartCoroutine(SpecialAttack());
	}

	void OnTriggerEnter(Collider other){
		print(other.gameObject.name);
		if (other.GetComponent<IDamageable>() != null && other.gameObject.tag != "Player") {
			// other.GetComponent<IDamageable>().TakeDamage(power);
			other.GetComponent<IDamageable>().TakeHit(power, other.transform.position, other.transform.position - transform.position);
			

		}
	}
}
