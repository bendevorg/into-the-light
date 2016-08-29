using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {

	public enum State {Walking, Dashing};
	State currentState;

	Vector3 velocity;
	Rigidbody myRigidbody;

	public float dashTime;
	public float dashSpeed;
	public float dashCooldown;
	bool canDash;

	SoundManager soundManager;

	void Start () {
		canDash = true;
		myRigidbody = GetComponent<Rigidbody>();
		soundManager = GetComponent<SoundManager>();
	}

	void Update(){
		if(Input.GetButton("Fire1") && canDash){
			StartCoroutine(Dash());
		}
	}
	
	public void Move(Vector3 _velocity) {
		velocity = _velocity;
	}

	void FixedUpdate(){
		if (currentState != State.Dashing){
			myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
		}
	}

	public void LookAt(Vector3 point){
		if (currentState != State.Dashing){
			Vector3 heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
			transform.LookAt(heightCorrectedPoint);
		}
	}

	IEnumerator Dash(){

		currentState = State.Dashing;
		float currentTime = 0;

		gameObject.layer = LayerMask.NameToLayer("Ghost");

		canDash = false;

		soundManager.Play();

		while(dashTime > currentTime){

			myRigidbody.MovePosition(myRigidbody.position + transform.forward * dashSpeed * Time.fixedDeltaTime);
			currentTime += Time.fixedDeltaTime;
			yield return new WaitForFixedUpdate();
		}

		currentState = State.Walking;
		gameObject.layer = LayerMask.NameToLayer("Player");

		yield return new WaitForSeconds(dashCooldown);

		canDash = true;

	}
}
