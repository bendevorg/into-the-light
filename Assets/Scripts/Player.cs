using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
public class Player : MonoBehaviour {

	public float moveSpeed;

	Camera viewCamera;
	PlayerController controller;
	Vector3 moveVelocity;
	// CameraFollow cameraScript;

	void Start () {
		controller = GetComponent<PlayerController>();
		viewCamera = Camera.main;
		// cameraScript = viewCamera.GetComponent<CameraFollow>();
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

	public Vector3 GetVelocity(){
		return moveVelocity;
	}
}
