using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for detecting objects within the player's sight.
public class ObjectDetecting : MonoBehaviour
{
    public static ObjectDetecting instance;

    [SerializeField] private float fieldOfViewAngle = 45f;

    private Vector3 directionToTarget, lookDirection;

    //Sets instance of detector
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// Finds if a target point is located within a specified degree field of view cone "cast" from the player's position.
    /// The relationship between target and player can be imagined as a right triangle(since magnitudes are normalized) with points at target direction, player forward, and player.
    /// This makes directionToTarget = hypotenuse
    /// And lookDirection = adjacent side
    /// Dot product of directionToTarget & lookDirection returns the cosine relationship of the two "sides".
    /// The arccosine of this cosine returns the angle between the target and the player from the player's point of view. This angle is converted from radians to degrees for easier comparison.
    /// </summary>
    /// <param name="targetPoint"> The target point to be checked if the player can see. </param>
    /// <returns>
    /// Returns true if the angle between the target and the player is smaller than the specified FOV angle.
    /// </returns>
    public bool CastFieldOfViewCone(Vector3 targetPoint)
    {
        directionToTarget = (targetPoint - transform.position).normalized;
        lookDirection = transform.forward;
        float cosAngle = Vector3.Dot(directionToTarget, lookDirection);
        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        return angle < fieldOfViewAngle;
    }

    //Draws field of view range
    public void OnDrawGizmos()
    {
        try
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Quaternion.Euler(0, fieldOfViewAngle, 0) * transform.forward * 1000f);
            Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -fieldOfViewAngle, 0) * transform.forward * 1000f);
        }
        catch (System.NullReferenceException)
        {

        }      
    }
}
