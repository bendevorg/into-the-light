using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable {

	public float startingHealth;
	protected float health;
	protected bool dead;

	protected virtual void Start () {
		health = startingHealth;
	}
	
	public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection){
		if (health > 0 && !dead) {
			health -= damage;
		}

		if (health <= 0 && !dead) {
			Die();
		}
	}

	protected void Die() {
		dead = true;
		GameObject.Destroy(gameObject);
	}
}
