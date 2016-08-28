﻿using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable {

	public float startingHealth;
	protected float health;
	protected bool dead;

	public event System.Action OnDeath;

	protected virtual void Start () {
		health = startingHealth;
	}
	
	public void TakeHit(float damage, RaycastHit hit){
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
