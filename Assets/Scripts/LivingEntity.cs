using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable {

	public float startingHealth;
	protected float health;
	protected bool dead;

	public event System.Action OnDeath;

	protected virtual void Start () {
		health = startingHealth;
	}
	
	public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection){
		
		TakeDamage(damage);
	}

	public virtual void TakeDamage(float damage) {
		CameraShaker.Shake(0.3f, 0.2f);
		if (health > 0 && !dead) {
			health -= damage;
		}

		if (health <= 0 && !dead) {
			Die();
		}
}

	public virtual void Die() {
		dead = true;
		OnDeath();
		GameObject.Destroy(gameObject);
	}
}
