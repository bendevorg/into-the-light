using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    Player player;
    Vector3 positionOffset;
    // Quaternion rotationOffset;

    public float distance = 15f;
    public float targetHeight = 1f;
	public float cameraSpeed = 0.035f;

    float x = 0;
    float y = 0;
    float z = 0;

    Vector3 playerVelocity;
	
    Camera mainCamera;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        // positionOffset = player.transform.position - transform.position;
        //  rotOffset = transform.rotation * Quaternion.Inverse(player.rotation);

        Vector3 cameraAngles = transform.eulerAngles;
        x = cameraAngles.x;
        y = cameraAngles.y;
		z = cameraAngles.z;

        // target = GameObject.FindGameObjectWithTag("Target").transform;

        // mainCamera = GetComponent<Camera>();

    }

    void Update(){
		playerVelocity = player.GetVelocity();

		// Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		// Plane groundPlane =  new Plane(Vector3.up, Vector3.zero);
		// float rayDistance;

		// if (groundPlane.Raycast(ray, out rayDistance)) {
		// 	Vector3 point = ray.GetPoint(rayDistance);
			// Debug.DrawLine(ray.origin, point, Color.red);
			// controller.LookAt(point);
		// }
    }

    // public void Move(Vector3 point){
    //     playerVelocity = point;
    // }

    void FixedUpdate() {
		// Debug.Log("X: " + x);
		// Debug.Log("Y: " + y);
		// Debug.Log("Z: " + z);
		
		// playerVelocity = player.GetVelocity();
		// y = player.eulerAngles.y;

		// ROTATE CAMERA:
		Quaternion rotation = Quaternion.Euler(x, y, z);
		transform.rotation = rotation;
        // Debug.Log(rotation);
        // Debug.Log(playerVelocity);

		// POSITION CAMERA:
        Vector3 offset = Vector3.right * playerVelocity.x;
		Vector3 newPosition = (player.transform.position + offset) - (rotation * Vector3.forward * distance + new Vector3(0, -targetHeight, 0));
		// transform.position = position;

        // transform.position = new Vector3(transform.position.x, transform.position.y, newPosition.z);
		transform.position = Vector3.Lerp(transform.position, newPosition, cameraSpeed);
        // transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed);
		// transform.position = Vector3.Lerp(transform.position, camTarget.transform.position, speed);
        // transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.transform.rotation, speed);


    }
}
