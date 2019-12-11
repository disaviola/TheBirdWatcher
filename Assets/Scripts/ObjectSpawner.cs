using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private TreeSpawnData treeSpawnData;
    [SerializeField] private BirdSpawnData birdSpawnData;

    private List<GameObject> Birds = new List<GameObject>();
    private GameObject[] treePrefabs, birdPrefabs;

    void Awake()
    {
        birdPrefabs = birdSpawnData.prefabs;
        treePrefabs = treeSpawnData.prefabs;
        SpawnTrees();
    }

    //Spawns trees within the specified area.
    private void SpawnTrees()
    {
        int spawnCount = 0;

        while (spawnCount < treeSpawnData.numberOfTreesToSpawn)
        {
            
            GameObject objectToSpawn = treePrefabs[Random.Range(0, treePrefabs.Length)];

            Vector3 position = new Vector3(Random.Range(-treeSpawnData.spawningRange_X, treeSpawnData.spawningRange_X), 100, Random.Range(-treeSpawnData.spawningRange_Z, treeSpawnData.spawningRange_Z));
            objectToSpawn = Instantiate(objectToSpawn, position, Quaternion.identity);

            objectToSpawn.name = objectToSpawn.name + spawnCount;

            RaycastHit hit;
            if (Physics.Raycast(objectToSpawn.transform.position, Vector3.down, out hit, Mathf.Infinity, treeSpawnData.areaToSpawnOn))
            {
                var distanceToGround = hit.distance;
                var currentPos = objectToSpawn.transform.position;
                var newY = currentPos.y - distanceToGround;
                objectToSpawn.transform.position = new Vector3(currentPos.x, newY, currentPos.z);
            }
            else
            {
                Destroy(objectToSpawn);
            }
            spawnCount++;
        }
        SpawnBirds();
    }

    //Spawns correct amount of birds and transports them to free landing points.
    private void SpawnBirds()
    {
        int spawnAmount1 = birdSpawnData.numberOfAnyTreeBirdsToSpawn;
        int spawnAmount2 = birdSpawnData.numberOfAnyTreeBirdsToSpawn;
        int spawnCount1 = 0, spawnCount2 = 0;

        while(spawnCount1 < spawnAmount1)
        {
            GameObject objectToSpawn = birdPrefabs[0];

            objectToSpawn = Instantiate(objectToSpawn, Vector3.zero, Quaternion.identity);

            objectToSpawn.name = objectToSpawn.name + spawnCount1;
            Birds.Add(objectToSpawn);
            spawnCount1++;
        }
        while (spawnCount2 < spawnAmount2)
        {
            GameObject objectToSpawn = birdPrefabs[1];

            objectToSpawn = Instantiate(objectToSpawn, Vector3.zero, Quaternion.identity);

            objectToSpawn.name = objectToSpawn.name + spawnCount2;
            Birds.Add(objectToSpawn);
            spawnCount2++;
        }
        foreach(GameObject bird in Birds)
        {
            if (bird.CompareTag("Bird_Any"))
            {
                bird.GetComponent<Bird_LandOnAnyTrees>().AddLandingPoints();
                bird.GetComponent<Bird_LandOnAnyTrees>().FlyToSpawn();
            } else if (bird.CompareTag("Bird_Leaf"))
            {
                bird.GetComponent<Bird_LandOnLeafTrees>().AddLandingPoints();
                bird.GetComponent<Bird_LandOnLeafTrees>().FlyToSpawn();
            }
        }

    }

    //Draws the tree spawning area
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(treeSpawnData.spawningRange_X * 2, 100, treeSpawnData.spawningRange_Z * 2));

    }
}
