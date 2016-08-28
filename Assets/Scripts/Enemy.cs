using UnityEngine;
using System.Collections;

public class Enemy : LivingEntity {

	Renderer rend;
	public Material black;
	public Material white;

	public ParticleSystem idleEffect;	
	public ParticleSystem hitEffect;
	public ParticleSystem deathEffect;
	

	protected override void Start () {
		base.Start();
		rend = GetComponent<Renderer>();
	}

	public override void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection)
	{
		float relativeHealth = 1 - (health/startingHealth);
		print(relativeHealth);
		rend.material.Lerp(black, white, relativeHealth);

		if (damage >= health && !dead) {
			idleEffect.Stop();
			deathEffect.Play();
			Destroy(this.gameObject, deathEffect.startLifetime + deathEffect.duration);
		} else if(!dead){
			hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hitDirection);
			hitEffect.Play();
		}

		base.TakeHit (damage, hitPoint, hitDirection);
	}
}
