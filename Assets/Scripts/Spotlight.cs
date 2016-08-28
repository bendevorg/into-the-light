using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CapsuleCollider))]
public class Spotlight : MonoBehaviour {

	public LayerMask collisionMask;
	CapsuleCollider spotlightCollider;
	Light spotlightLight;

	public float maxIntensity;
	public float maxAngle;
	public float speedToCreate;

	[Range(0,100)]
	public float bordinhaPercentual;

	public void Create(float duration){
		StartCoroutine("CreateSpotlight", duration);
	}

	IEnumerator CreateSpotlight(float duration){

		spotlightCollider = GetComponent<CapsuleCollider>();
		spotlightLight = GetComponent<Light>();
		spotlightLight.intensity = 0;
		spotlightLight.spotAngle = 0;

		Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, spotlightLight.range, collisionMask, QueryTriggerInteraction.Ignore)){

			CreateCollider(hit.distance);

        }

		while(Mathf.Round(spotlightLight.intensity) != Mathf.Round(maxIntensity) && Mathf.Round(spotlightLight.spotAngle) != Mathf.Round(maxAngle)){

			spotlightLight.intensity = Mathf.Lerp(spotlightLight.intensity, maxIntensity, speedToCreate);
			spotlightLight.spotAngle = Mathf.Lerp(spotlightLight.spotAngle, maxAngle, speedToCreate);
			yield return null;

		}
		yield return StartCoroutine("DestroySpotlight", duration);

	}

	IEnumerator DestroySpotlight(float duration){

		yield return new WaitForSeconds(duration);

		print('b');

		while(spotlightLight.intensity > 0 && spotlightLight.spotAngle > 0){

			print('a');
			spotlightLight.intensity = Mathf.Lerp(spotlightLight.intensity, 0, speedToCreate);
			spotlightLight.spotAngle = Mathf.Lerp(spotlightLight.spotAngle, 0, speedToCreate);
			yield return null;

		}
		Destroy(this.gameObject);
		yield return null;

	}
	
	void CreateCollider(float distance){
		spotlightCollider.center = new Vector3(spotlightCollider.center.x, spotlightCollider.center.y, spotlightCollider.center.z + distance);
		float tan = Mathf.Tan(Mathf.Deg2Rad * (maxAngle/2));
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
