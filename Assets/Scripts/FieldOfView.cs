using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Light))]
public class FieldOfView : MonoBehaviour {

	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

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

	void Update(){
		
		if(mirror.mirrorLight.enabled){
			viewRadius = mirror.mirrorLight.range;
			viewAngle = mirror.mirrorLight.spotAngle;
			DrawFieldOfView();
		}
	}

	void Start(){
		StartCoroutine ("FindTargetsWithDelay", .2f);
	}

	void FindVisibleTargets() {
		visibleTargets.Clear ();
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.position);

				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					visibleTargets.Add (target);
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

		if (Physics.Raycast(transform.position, transform.position + DirFromAngle(angle,true) * viewRadius, out hit, viewRadius, obstacleMask) ){
			Debug.DrawLine(transform.position, hit.point, Color.red);
			print(hit.collider.name);	
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
