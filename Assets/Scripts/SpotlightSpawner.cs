using UnityEngine;
using System.Collections;

public class SpotlightSpawner : MonoBehaviour {

	public float spotlightHeight;
	public Spotlight spotlight;

	public float minSpawnTime;
	public float maxSpawnTime;

	public float minDurationTime;
	public float maxDurationTime;

	MapGenerator maps;

	// Use this for initialization
	void Start () {
		maps = GameObject.FindGameObjectWithTag("Map").GetComponent<MapGenerator>();
		StartCoroutine(SpawnController());
	}

	void SpawnSpotlight(Vector3 spotlightPosition, float duration){
		Spotlight newSpotlight = Instantiate(spotlight, spotlightPosition, Quaternion.Euler(Vector3.right * 90)) as Spotlight;
		newSpotlight.Create(duration);
	}

	IEnumerator SpawnController(){

		while(true){
			yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
			SpawnSpotlight(new Vector3(Random.Range(-maps.currentMap.mapSize.x/2,maps.currentMap.mapSize.x/2), spotlightHeight, Random.Range(-maps.currentMap.mapSize.y/2,maps.currentMap.mapSize.y/2)), Random.Range(minDurationTime, maxDurationTime));
		}

	}

}
