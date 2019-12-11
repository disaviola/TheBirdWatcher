using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class for detecting objects within the player's sight.
 * Detected objects are non-specified
 */
public class ObjectDetecting : MonoBehaviour
{
    public static ObjectDetecting instance;

    [SerializeField] private float fieldOfViewAngle = 45f;

    private Vector3 point1, point2;

    //Sets instance of detector
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //Finds if a target point is within a field of view cone cast from the player's position.
    //Finds the cosine angle between the player and the target by using the dot product of the distance between the player's position and the target point,
    //and the forward vector (look direction) of the player's position.
    //Gets the angle of distance between player and target from the acosine (inverse) of the cosine, and converts from radians to degrees for easier comparison/readability.
    //Returns true if distance angle is smaller than FOV angle, e.g the target point is within the specified field of view angle.

        //point1 = hypotenuse
        //point2 = adjacent side
        //Dot product of point 1 & 2 returns cosine angle between target and player from the target's point of view.
        //Acosine of the cosine returns the angle between target and player from the player's point of view. This angle is converted from radians to degrees for easier comparison.
        //Returns true if the angle between the target and the player is smaller than the specified FOV angle.

    public bool CastFieldOfViewCone(Vector3 targetPoint)
    {
        point1 = (targetPoint - transform.position).normalized;
        point2 = transform.forward;
        float cosAngle = Vector3.Dot(point1, point2);
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
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -fieldOfViewAngle, 0) * transform.forward * 1000f);
        }
        catch (System.NullReferenceException)
        {

        }      
    }
}
