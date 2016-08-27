using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    // public float distanceToPlayer = 5;

    Transform player;
    Vector3 posOffset;
    Quaternion rotOffset;

    Transform target;
    public float distance = 5f;
    public float targetHeight = 1.2f;
	public float speed = 0.5f;

    float x = 0;
    float y = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        posOffset = player.position - transform.position;
        //  rotOffset = transform.rotation * Quaternion.Inverse(player.rotation);

        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;

        // target = GameObject.FindGameObjectWithTag("Target").transform;

    }

    void FixedUpdate() {
		Debug.Log("X: " + x);
		Debug.Log("Y: " + y);

		y = player.eulerAngles.y;

		// ROTATE CAMERA:
		Quaternion rotation = Quaternion.Euler(x, y, 0);
		// transform.rotation = rotation;

		// POSITION CAMERA:
		Vector3 position = player.position - (rotation * Vector3.forward * distance + new Vector3(0, -targetHeight, 0));
		// transform.position = position;

		transform.position = Vector3.Lerp(transform.position, position, speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed);
		// transform.position = Vector3.Lerp(transform.position, camTarget.transform.position, speed);
        // transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.transform.rotation, speed);


    }
}
