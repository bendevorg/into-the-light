using UnityEngine;
using System.Collections;

public class SpotlightSpawner : MonoBehaviour {

	public float spotlightHeight;
	public Spotlight spotlight;

	public float minSpawnTime;
	public float maxSpawnTime;

	public float minDurationTime;
	public float maxDurationTime;

	public float maxTilesDistanceFromThePlayer;

	Transform playerT;

	MapGenerator maps;

	// Use this for initialization
	void Start () {
		maps = GameObject.FindGameObjectWithTag("Map").GetComponent<MapGenerator>();
		playerT = GameObject.FindGameObjectWithTag("Player").transform;
		StartCoroutine(SpawnController());
	}

	void SpawnSpotlight(Vector3 spotlightPosition, float duration){
		Spotlight newSpotlight = Instantiate(spotlight, spotlightPosition, Quaternion.Euler(Vector3.right * 90)) as Spotlight;
		newSpotlight.Create(duration);
	}

	IEnumerator SpawnController(){

		float maxXPosition;
		float minXPosition;
		float maxYPosition;
		float minYPosition;

		while(true){

			if(maps.canSpawn) {

				yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

				minXPosition = playerT.position.x - maxTilesDistanceFromThePlayer * maps.tileSize > -maps.currentMap.mapSize.x/2 ? playerT.position.x - maxTilesDistanceFromThePlayer * maps.tileSize : -maps.currentMap.mapSize.x/2;
				maxXPosition = playerT.position.x + maxTilesDistanceFromThePlayer * maps.tileSize < maps.currentMap.mapSize.x/2 ? playerT.position.x + maxTilesDistanceFromThePlayer * maps.tileSize : maps.currentMap.mapSize.x/2;

				minYPosition = playerT.position.z - maxTilesDistanceFromThePlayer * maps.tileSize > -maps.currentMap.mapSize.y/2 ? playerT.position.z - maxTilesDistanceFromThePlayer * maps.tileSize : -maps.currentMap.mapSize.y/2;
				maxYPosition = playerT.position.z + maxTilesDistanceFromThePlayer * maps.tileSize < maps.currentMap.mapSize.y/2 ? playerT.position.z + maxTilesDistanceFromThePlayer * maps.tileSize : maps.currentMap.mapSize.y/2;

				SpawnSpotlight(new Vector3(Random.Range(minXPosition,maxXPosition), spotlightHeight, Random.Range(minYPosition,maxYPosition)), Random.Range(minDurationTime, maxDurationTime));
			
			}
			
		}

	}

}
