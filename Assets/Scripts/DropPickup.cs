using UnityEngine;
using System.Collections;

public class DropPickup : MonoBehaviour {

	public Pickup pickup;
	public float lifeTime;
	public int pickupAmountPerDrop;

	float pickupRadius;

	// Use this for initialization
	void Start () {
	
		GetComponent<LivingEntity>().OnDeath += CreatePickups;
		pickupRadius = pickup.GetComponent<SphereCollider>().radius;

	}


	void CreatePickups(){

		while (pickupAmountPerDrop > 0){
			Vector3 position = new Vector3(transform.position.x + pickupRadius + Random.Range(0.0f, pickupRadius * 5.0f), transform.position.y + pickupRadius, transform.position.z + pickupRadius + Random.Range(0.0f, pickupRadius * 5.0f));
			Pickup newPickup = Instantiate(pickup, position, Quaternion.identity) as Pickup;
			newPickup.Lifetime(lifeTime);
			pickupAmountPerDrop--;
		}

	}

}
