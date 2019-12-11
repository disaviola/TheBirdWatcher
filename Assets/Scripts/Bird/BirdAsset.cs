using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public enum BirdBehaviour
{
	LandOnTreesWLeaves,
	LandOnAll
}

[CreateAssetMenu(fileName = "BirdAsset", menuName = "ScriptableObjects/BirdAsset")]
public class BirdAsset : ScriptableObject
{
	public AnimatorController AnimController;
	public Color BirdColor;
	public BirdBehaviour Behaviour;
    [Tooltip("The flight speed of the bird.")]
    public float flightSpeed = 5f;
    [Tooltip("The amount of time (in seconds) the bird needs to be observed by the player before it is alerted.")]
    public float observationTime = 1.5f;
    public GameObject BirdPrefab_A;
}
