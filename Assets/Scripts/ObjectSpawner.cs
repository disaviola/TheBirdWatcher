using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private TreeSpawnData treeSpawnData;

    private List<GameObject> spawnedObjects;
    private GameObject[] prefabs;
    //private GameObject objectToSpawn;

    void Awake()
    {
        prefabs = treeSpawnData.prefabs;
        Spawn();
    }

    private void Spawn()
    {
        int spawnCount = 0;

        while (spawnCount < treeSpawnData.numberOfTreesToSpawn)
        {
            
            GameObject objectToSpawn = prefabs[Random.Range(0, prefabs.Length)];

            Vector3 position = new Vector3(Random.Range(0, treeSpawnData.spawningRange_X), 100, Random.Range(0, treeSpawnData.spawningRange_Z));
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

            //spawnedObjects.Add(objectToSpawn);
            spawnCount++;
            Debug.Log("Spawned " + objectToSpawn.name);
        }

        //RaycastHit hit;
        //foreach(GameObject spawnedObject in spawnedObjects)
        //{
        //    if (Physics.Raycast(spawnedObject.transform.position, Vector3.down, out hit, Mathf.Infinity, treeSpawnData.areaToSpawnOn))
        //    {
        //        var distanceToGround = hit.distance;
        //        var currentPos = spawnedObject.transform.position;
        //        var newY = currentPos.y - distanceToGround;
        //        spawnedObject.transform.position = new Vector3(currentPos.x, newY, currentPos.z);
        //    }
        //}
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(treeSpawnData.spawningRange_X, 100, treeSpawnData.spawningRange_Z));
    }
}
