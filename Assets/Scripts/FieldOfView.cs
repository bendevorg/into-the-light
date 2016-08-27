using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Light))]
public class FieldOfView : MonoBehaviour {

	public float viewRadius;
	[HideInInspector]
	public float viewAngle;

	public LayerMask hittableMask;
	public LayerMask mirrorMask;

	[HideInInspector]
	public List<Transform> visibleTargets;

	public float meshResolution;
	Mirror mirror;

	void Awake() {
		if(GetComponent<Mirror>() != null){
			mirror = GetComponent<Mirror>();
		}
	}

	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}

	void Start(){
		//FindTargets += FindVisibleTargets;
		StartCoroutine ("FindTargetsWithDelay", .2f);
	}

	void FindVisibleTargets() {
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, hittableMask);
	
		if (mirror.mirrorLight.enabled){

			viewAngle = mirror.mirrorLight.spotAngle;
			visibleTargets = new List<Transform>();

			DrawFieldOfView();

			for (int i = 0; i < targetsInViewRadius.Length; i++) {
				bool targetInFieldOfViews = false;

				for(int j = 0; j < visibleTargets.Count; j++){
					if(targetsInViewRadius[i].transform.position == visibleTargets[j].position){
						targetInFieldOfViews = true;
						break;
					}
				}

				if(!targetInFieldOfViews){
					if(targetsInViewRadius[i].GetComponent<Mirror>() != null && targetsInViewRadius[i].GetComponent<Mirror>().mirrorLight.enabled){
						targetsInViewRadius[i].GetComponent<Mirror>().Reflect(false);
					}
				}

			}
		}

	}

	void DrawFieldOfView(){

		int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
		float stepAngleSize = viewAngle/stepCount;

		for(int i=0; i<=stepCount;i++){

			float angle = transform.eulerAngles.y - viewAngle/2 + stepAngleSize * i;
			RaycastHit hit;

			if (Physics.Raycast(transform.position, transform.position + DirFromAngle(angle,true) * viewRadius, out hit, viewRadius, mirrorMask) ){
				Debug.DrawLine(transform.position, hit.point, Color.red);
				visibleTargets.Add(hit.collider.transform);
				if (hit.collider.GetComponent<Mirror>() != null){
					hit.collider.GetComponent<Mirror>().Reflect(true);
				}	
			}	
		}
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

}
