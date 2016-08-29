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

	public GameObject specialGameObject;
	public float specialMaxSize;
	public float specialSpeed;
	public float specialCooldown;
	bool canSpecial;
	

	void Start () {
		canDash = true;
		canSpecial = true;
		myRigidbody = GetComponent<Rigidbody>();
	}

	void Update(){
		if(Input.GetButton("Fire1") && canDash){
			StartCoroutine(Dash());
		}
		if(Input.GetButton("Fire2") && canSpecial){
			StartCoroutine(Special());
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

	IEnumerator Special(){
		float timer = 0;

		canSpecial = false;

		GameObject special = Instantiate(specialGameObject, transform.position, transform.rotation) as GameObject;

		while(specialMaxSize > special.transform.localScale.x){

			// special.transform.localScale = Vector3.Lerp(special.transform.localScale, );
			special.transform.localScale += new Vector3(1, 1, 1) * Time.fixedDeltaTime * specialSpeed;
			Debug.Log("Local scale: " + special.transform.localScale);
			timer += Time.fixedDeltaTime;
			yield return null;
		}

		Destroy(special);

		canSpecial = true;
		
		// yield return null;
	}
}
