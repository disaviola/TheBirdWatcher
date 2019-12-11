using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreeSpawnData", menuName = "ScriptableObjects/TreeSpawnData")]
public class TreeSpawnData : ScriptableObject
{
    //public string treePrefabName;
    public GameObject[] prefabs;
    [Tooltip("Number of objects to spawn initially. Objects spawn randomly, objects not spawning on the correct areas will be destroyed.")]
    public int numberOfTreesToSpawn;
    [Tooltip("Areas within the spawn range that the objects will spawn on.")]
    public LayerMask areaToSpawnOn;
    [Tooltip("Objects will randomly spawn in a X by Z area around center of map and snap to the surface.")]
    public int spawningRange_X, spawningRange_Z;

}
