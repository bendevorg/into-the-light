using UnityEngine;
using System.Collections;

public class Enemy : LivingEntity {

	Renderer rend;
	public Material black;
	public Material white;

	public ParticleSystem hitEffect;

	protected override void Start () {
		base.Start();
		rend = GetComponent<Renderer>();
	}

	public override void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection)
	{
		hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hitDirection);
		hitEffect.Play();
		float relativeHealth = 1 - (health/startingHealth);
		print(relativeHealth);
		rend.material.Lerp(black, white, relativeHealth);

		base.TakeHit (damage, hitPoint, hitDirection);
	}
}
