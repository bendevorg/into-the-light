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
	float attackDistanceThreshold = 5.5f;

	public float attackCooldown;

	bool hasTarget;

	AttackController attackController;

	protected override void Start () {
		base.Start();
		rend = GetComponent<Renderer>();

		pathfinder = GetComponent<NavMeshAgent> ();
		attackController = GetComponent<AttackController>();

		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			currentState = State.Chasing;
			hasTarget = true;

			target = GameObject.FindGameObjectWithTag ("Player").transform;
			targetEntity = target.GetComponent<LivingEntity> ();
			targetEntity.OnDeath += OnTargetDeath;

			myCollisionRadius = GetComponent<CapsuleCollider> ().radius;
			targetCollisionRadius = target.GetComponent<CapsuleCollider> ().radius;

			StartCoroutine (UpdatePath ());
			StartCoroutine (Attack ());
		}
	}

	void Update(){
		transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
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
			GameObject deathEffectObject = Instantiate(deathEffect.gameObject, transform.position, deathEffect.transform.rotation) as GameObject;

			Destroy(deathEffectObject, deathEffect.startLifetime + deathEffect.duration);
			idleEffect.transform.parent = deathEffectObject.transform;
			idleEffect.Stop();
			hitEffect.transform.parent = deathEffectObject.transform;
			hitEffect.Stop();
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
					transform.LookAt(targetPosition);
				}
			}
			yield return new WaitForSeconds(refreshRate);
		}
	}

	IEnumerator Attack(){

		while (hasTarget){

			yield return new WaitForSeconds(attackCooldown);
			currentState = State.Attacking;

			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (!dead){

				attackController.Shoot();
				yield return null;

			}

		}

	}

}
