using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    //[SerializeField] private TreeSpawnData treeSpawnData;
    public GameObject[] prefabs;
    [Tooltip("Number of objects to spawn initially. Objects spawn randomly, objects not spawning on the correct areas will be destroyed.")]
    public int numberOfTreesToSpawn;
    [Tooltip("Areas within the spawn range that the objects will spawn on.")]
    public LayerMask areaToSpawnOn;
    [Tooltip("Objects will randomly spawn in a X by Z area around center of map and snap to the surface.")]
    public int spawningRange_X, spawningRange_Z;


    //private GameObject[] treePrefabs; //birdPrefabs;

    void Awake()
    {
        SpawnTrees();
    }

    //Spawns trees within the specified area.
    private void SpawnTrees()
    {
        int spawnCount = 0;

        while (spawnCount < numberOfTreesToSpawn)
        {
            
            GameObject objectToSpawn = prefabs[Random.Range(0, prefabs.Length)];

            Vector3 position = new Vector3(Random.Range(-spawningRange_X, spawningRange_X), 100, Random.Range(-spawningRange_Z, spawningRange_Z));
            objectToSpawn = Instantiate(objectToSpawn, position, Quaternion.identity);

            objectToSpawn.name = objectToSpawn.name + spawnCount;

            RaycastHit hit;
            if (Physics.Raycast(objectToSpawn.transform.position, Vector3.down, out hit, Mathf.Infinity, areaToSpawnOn))
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
        //SpawnBirds();
    }

    //Spawns correct amount of birds and transports them to free landing points.
    //private void SpawnBirds()
    //{
    //    int spawnAmount1 = birdSpawnData.numberOfAnyTreeBirdsToSpawn;
    //    int spawnAmount2 = birdSpawnData.numberOfAnyTreeBirdsToSpawn;
    //    int spawnCount1 = 0, spawnCount2 = 0;

    //    while(spawnCount1 < spawnAmount1)
    //    {
    //        GameObject objectToSpawn = birdPrefabs[0];

    //        objectToSpawn = Instantiate(objectToSpawn, Vector3.zero, Quaternion.identity);

    //        objectToSpawn.name = objectToSpawn.name + spawnCount1;
    //        Birds.Add(objectToSpawn);
    //        spawnCount1++;
    //    }
    //    while (spawnCount2 < spawnAmount2)
    //    {
    //        GameObject objectToSpawn = birdPrefabs[1];

    //        objectToSpawn = Instantiate(objectToSpawn, Vector3.zero, Quaternion.identity);

    //        objectToSpawn.name = objectToSpawn.name + spawnCount2;
    //        Birds.Add(objectToSpawn);
    //        spawnCount2++;
    //    }
    //    foreach(GameObject bird in Birds)
    //    {
    //        if (bird.CompareTag("Bird_Any"))
    //        {
    //            bird.GetComponent<Bird_LandOnAnyTrees>().AddLandingPoints();
    //            bird.GetComponent<Bird_LandOnAnyTrees>().FlyToSpawn();
    //        } else if (bird.CompareTag("Bird_Leaf"))
    //        {
    //            bird.GetComponent<Bird_LandOnLeafTrees>().AddLandingPoints();
    //            bird.GetComponent<Bird_LandOnLeafTrees>().FlyToSpawn();
    //        }
    //    }

    //}

    //Draws the tree spawning area
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(spawningRange_X * 2, 100, spawningRange_Z * 2));

    }
}
