using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]
public class Enemy : LivingEntity {
	
	public enum State {Idle, Chasing, Attacking};
	State currentState;
	NavMeshAgent pathfinder;
	Transform target;
	LivingEntity targetEntity;

	Renderer rend;
	public Material black;
	public Material white;

	public ParticleSystem idleEffect;	
	public ParticleSystem hitEffect;
	public ParticleSystem deathEffect;

	float myCollisionRadius;
	float targetCollisionRadius;

	// Ajustar isso dependendo do range do carinha
	float attackDistanceThreshold = .5f;

	bool hasTarget;

	protected override void Start () {
		base.Start();
		rend = GetComponent<Renderer>();

		pathfinder = GetComponent<NavMeshAgent> ();

		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			currentState = State.Chasing;
			hasTarget = true;

			target = GameObject.FindGameObjectWithTag ("Player").transform;
			targetEntity = target.GetComponent<LivingEntity> ();
			targetEntity.OnDeath += OnTargetDeath;

			myCollisionRadius = GetComponent<CapsuleCollider> ().radius;
			targetCollisionRadius = target.GetComponent<CapsuleCollider> ().radius;

			StartCoroutine (UpdatePath ());
		}
	}

	void OnTargetDeath() {
		hasTarget = false;
		currentState = State.Idle;
	}

	public override void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection)
	{
		float relativeHealth = 1 - (health/startingHealth);
		//print(relativeHealth);
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

	IEnumerator UpdatePath() {
		float refreshRate = .25f;

		while (hasTarget) {
			if (currentState == State.Chasing) {
				Vector3 dirToTarget = (target.position - transform.position).normalized;
				Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2);
				if (!dead) {
					pathfinder.SetDestination (targetPosition);
				}
			}
			yield return new WaitForSeconds(refreshRate);
		}
	}

}
