using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (PlayerController))]
public class Player : LivingEntity {

	public float moveSpeed;

	Camera viewCamera;
	PlayerController controller;
	Vector3 moveVelocity;
	// CameraFollow cameraScript;
	public ParticleSystem hitEffect;
	Renderer renderer;
	public Material black;
	public Material white;

	protected override void Start () {
		base.Start();
		controller = GetComponent<PlayerController>();
		viewCamera = Camera.main;
		// cameraScript = viewCamera.GetComponent<CameraFollow>();
		renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		moveVelocity = moveInput.normalized * moveSpeed;
		// Debug.Log(moveVelocity);
		controller.Move(moveVelocity);

		Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane =  new Plane(Vector3.up, Vector3.zero);
		float rayDistance;

		if (groundPlane.Raycast(ray, out rayDistance)) {
			Vector3 point = ray.GetPoint(rayDistance);
			// Debug.DrawLine(ray.origin, point, Color.red);
			controller.LookAt(point);
			// cameraScript.Move(point);
			
		}
	}

	public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection){

		if (!dead) {

			GameObject hitEffectObject = Instantiate(hitEffect.gameObject, transform.position, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject;
			Destroy(hitEffectObject, hitEffect.duration + hitEffect.startLifetime);

			if (damage >= health) {
				//death particles
			} else {
				ChangeHealthColor();
			}
			
			base.TakeHit(damage, hitPoint, hitDirection);
		}
	}

	void ChangeHealthColor(){
		float relativeHealth = 1 - (health/startingHealth);
		renderer.material.Lerp(white, black, relativeHealth);
	}

	public Vector3 GetVelocity(){
		return moveVelocity;
	}

	public override void Die(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
