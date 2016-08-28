using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour {

	public Projectile projectile;

	public float attackSpeed;
	public float circleSpeedDiscount = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shoot(){

		int attack = Random.Range(0,3);

		switch (attack){

			case 0:
				StartCoroutine(LineShoot());
				break;

			case 1:
				WaveShoot();
				break;

			case 2:
				ThreeSixtyNoScope();
				break;

		}

		Projectile newProjectile;
		newProjectile = Instantiate(projectile, transform.position, transform.rotation) as Projectile;
		newProjectile.SetSpeed(attackSpeed);

	}

	IEnumerator LineShoot(){
		
		int amount = Random.Range(1, 5);

		while (amount > 0){

			Projectile newProjectile;
			newProjectile = Instantiate(projectile, transform.position, transform.rotation) as Projectile;
			newProjectile.SetSpeed(attackSpeed);
			yield return new WaitForSeconds(0.1f);
			amount--;

		}
		yield return null;
	}

	void WaveShoot(){

		int amount = Random.Range(1, 3);
		int count = 1;
		float angle = 10f;

		while (count<=amount){

			Projectile newProjectile;
			newProjectile = Instantiate(projectile, transform.position, Quaternion.Euler(0f, transform.rotation.y + angle * count, 0f)) as Projectile;
			newProjectile.SetSpeed(attackSpeed);

			newProjectile = Instantiate(projectile, transform.position, Quaternion.Euler(0f, transform.rotation.y - angle * count, 0f)) as Projectile;
			newProjectile.SetSpeed(attackSpeed/10);

			count ++;

		}
		

	}

	void ThreeSixtyNoScope(){

		float angle = 0;

		while (angle < 360){

			Projectile newProjectile = Instantiate(projectile, transform.position, Quaternion.Euler(0f, angle, 0f)) as Projectile;
			newProjectile.SetSpeed(attackSpeed/circleSpeedDiscount);
			angle += 10;

		}

	}

	

}
