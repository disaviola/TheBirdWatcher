using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Animations;

//Drop-down behaviour picker to set bird behaviour.
public enum BirdBehaviour
{
	LandOnTreesWLeaves,
	LandOnAll
}

//Bird asset creator. Used for creating different kinds of bird assets with different properties and behaviours.
[CreateAssetMenu(fileName = "BirdAsset", menuName = "ScriptableObjects/BirdAsset")]
public class BirdAsset : ScriptableObject
{
	public RuntimeAnimatorController AnimController;
	public Color BirdColor;
	public BirdBehaviour Behaviour;
    [Tooltip("The flight speed of the bird.")]
    public float flightSpeed = 5f;
    [Tooltip("The amount of time (in seconds) the bird needs to be observed by the player before it is alerted.")]
    public float observationTime = 1.5f;
    public GameObject BirdPrefab;
}
