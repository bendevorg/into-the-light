using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    public float speed = 0.1f;
    // public bool freeCam = true;

    Transform camTarget;
    Transform target;
    Transform player;
    // RaycastHit raycastHit;
    // LayerMask playerLayer;
    // SphereCollider sphereCollider;

    void Start()
    {
        // playerLayer = LayerMask.GetMask("Player");
        camTarget = GameObject.FindGameObjectWithTag("CamTarget").transform;
        // target = GameObject.FindGameObjectWithTag("Target").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // sphereCollider = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SphereCollider>();

    }

    // void Update()
    // {
    //     if (Input.GetKeyDown("space"))
    //     {
    //         freeCam = !freeCam;
    //     }
    // }

    void FixedUpdate()
    {
        // transform.position = Vector3.Lerp(transform.position, camTarget.transform.position, speed);
        transform.position = Vector3.Lerp(transform.position, camTarget.transform.position, speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.transform.rotation, speed);

        // transform.position = camTarget.transform.position;
        // transform.rotation = camTarget.transform.rotation;
    }
}