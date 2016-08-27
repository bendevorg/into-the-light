using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
public class Player : MonoBehaviour {

	public float moveSpeed;
	PlayerController controller;
	Vector3 moveVelocity;

	void Start () {
		controller = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		moveVelocity = moveInput.normalized * moveSpeed;
		// Debug.Log(moveVelocity);
		controller.Move(moveVelocity);
	}

	public Vector3 GetVelocity(){
		return moveVelocity;
	}
}
