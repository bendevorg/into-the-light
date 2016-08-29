using UnityEngine;
using System.Collections;

public class DropPickup : MonoBehaviour {

	public Pickup pickup;
	public float lifeTime;
	public int maxPickupAmountPerDrop;
	public int minPickupAmountPerDrop;
	int pickupAmountPerDrop;

	public int pickupPoints;

	float pickupRadius;

	// Use this for initialization
	void Start () {
	
		GetComponent<LivingEntity>().OnDeath += CreatePickups;
		pickupRadius = pickup.GetComponent<SphereCollider>().radius;
		pickupAmountPerDrop = Random.Range(minPickupAmountPerDrop, maxPickupAmountPerDrop);

	}


	void CreatePickups(){

		while (pickupAmountPerDrop > 0){

			Vector3 position = new Vector3(transform.position.x + pickupRadius + Random.Range(0.0f, pickupRadius * 5.0f), pickupRadius/2, transform.position.z + pickupRadius + Random.Range(0.0f, pickupRadius * 5.0f));
			Pickup newPickup = Instantiate(pickup, position, Quaternion.identity) as Pickup;
			newPickup.Lifetime(lifeTime);
			newPickup.Points(pickupPoints);
			pickupAmountPerDrop--;

		}

	}

}
