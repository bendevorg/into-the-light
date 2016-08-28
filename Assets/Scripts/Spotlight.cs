using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CapsuleCollider))]
public class Spotlight : MonoBehaviour {

	public LayerMask collisionMask;
	CapsuleCollider spotlightCollider;
	Light spotlightLight;

	[Range(0,100)]
	public float bordinhaPercentual;

	// Use this for initialization
	void Start () {

		spotlightCollider = GetComponent<CapsuleCollider>();
		spotlightLight = GetComponent<Light>();
		Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, spotlightLight.range, collisionMask, QueryTriggerInteraction.Ignore)){

			print(hit.distance);
			CreateCollider(hit.distance);

        }
	}
	
	void CreateCollider(float distance){
		spotlightCollider.center = new Vector3(spotlightCollider.center.x, spotlightCollider.center.y, spotlightCollider.center.z + distance);
		float tan = Mathf.Tan(Mathf.Deg2Rad * (spotlightLight.spotAngle/2));
		float radius = tan * distance;

		spotlightCollider.radius = radius * (1-(bordinhaPercentual/100));
	}

	void OnTriggerEnter(Collider collider) {
        if (collider.GetComponent<MirrorController>()) {
			collider.GetComponent<MirrorController>().SetLight(true);
		}
    }

	void OnTriggerExit(Collider collider) {
        if (collider.GetComponent<MirrorController>()) {
			collider.GetComponent<MirrorController>().SetLight(false);
		}
    }
}
