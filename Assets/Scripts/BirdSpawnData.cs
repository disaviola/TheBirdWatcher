using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BirdSpawnData", menuName = "ScriptableObjects/BirdSpawnData")]
public class BirdSpawnData : ScriptableObject
{
    public GameObject[] prefabs;
    [Tooltip("Number of birds who fly to any tree to spawn initially.")]
    public int numberOfAnyTreeBirdsToSpawn;
    [Tooltip("Number of birds who fly to leaf trees to spawn initially.")]
    public int numberOfLeafTreeBirdsToSpawn;

    [HideInInspector] public List<GameObject> Birds;
}
