using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    [Tooltip("Number of trees to spawn. Trees spawn randomly within spawn area, trees not spawning on the specified layer objects will be destroyed.")]
    public int numberOfTreesToSpawn;
    [Tooltip("Objects within the spawn range that the trees will spawn on.")]
    public LayerMask areaToSpawnOn;
    [Tooltip("Objects will randomly spawn in a X by Z area around center of map and snap to the surface.")]
    public int spawningRange_X, spawningRange_Z;

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
    }

    //Draws the tree spawning area
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(spawningRange_X * 2, 100, spawningRange_Z * 2));

    }
}
