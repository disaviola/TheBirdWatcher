using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BirdPropertyData", menuName = "ScriptableObjects/BirdPropertyData")]
public class BirdPropertyData : ScriptableObject
{
    [Tooltip("The flight speed of the bird.")]
    [SerializeField] public float flightSpeed = 5f;
    [Tooltip("The amount of time (in seconds) the bird needs to be observed by the player before it is alerted.")]
    [SerializeField] public float observationTime = 1.5f;
}
