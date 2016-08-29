using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(AudioSource))]
public class Spotlight : MonoBehaviour {

	public LayerMask collisionMask;
	public LayerMask lightMask;
	CapsuleCollider spotlightCollider;
	Light spotlightLight;

	public float maxIntensity;
	public float maxAngle;
	public float speedToCreate;
	public float speedToDestroy;
	
	Transform player;

	float maxRadius;

	bool hasPlayer = false;

	[Range(0,100)]
	public float bordinhaPercentual;

	SoundManager soundManager;

	public void Create(float duration){
		player = GameObject.FindGameObjectWithTag("Player").transform;
		soundManager = GetComponent<SoundManager>();
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

		if (soundManager.playing){
			soundManager.Stop();
		}

		while(Mathf.Round(spotlightLight.intensity) != 0 && Mathf.Round(spotlightLight.spotAngle) != 0){
			spotlightLight.intensity = Mathf.Lerp(spotlightLight.intensity, 0, speedToDestroy);
			spotlightLight.spotAngle = Mathf.Lerp(spotlightLight.spotAngle, 0, speedToDestroy);
			yield return null;

		}
        
		if (hasPlayer){
			player.GetComponent<MirrorController>().SetLight(false);
		}

		Destroy(this.gameObject);
		yield return null;

	}
	
	void CreateCollider(float distance){
		spotlightCollider.center = new Vector3(spotlightCollider.center.x, spotlightCollider.center.y, spotlightCollider.center.z + distance);
		float tan = Mathf.Tan(Mathf.Deg2Rad * (maxAngle/2));
		float radius = tan * distance;

		spotlightCollider.radius = radius * (1-(bordinhaPercentual/100));
		maxRadius = spotlightCollider.radius;
	}

	void OnTriggerEnter(Collider collider) {
        if (collider.GetComponent<MirrorController>()) {
			collider.GetComponent<MirrorController>().SetLight(true);
			hasPlayer = true;

			if (!soundManager.playing){
				soundManager.Play();
			}
		} 
    }

	void OnTriggerStay(Collider collider) {
        if (collider.GetComponent<MirrorController>()) {
			collider.GetComponent<MirrorController>().SetLight(true);
			hasPlayer = true;
			if (!soundManager.playing){
				soundManager.Play();
			}
		}

    }

	void OnTriggerExit(Collider collider) {
        if (collider.GetComponent<MirrorController>()) {
			collider.GetComponent<MirrorController>().SetLight(false);
			hasPlayer = false;
		} 
    }
}
