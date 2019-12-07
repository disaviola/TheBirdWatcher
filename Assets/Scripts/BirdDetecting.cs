using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdDetecting : MonoBehaviour
{
    public static BirdDetecting instance;

    public float fieldOfViewAngle = 45f;

    private Vector3 point1, point2;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //Find if a target point is within a field of view cone
    //using the dot product of the distance between the camera and the target point and the forward vector of the camera.
    //Get the angle of the difference and convert to degrees for easier comparison/reading
    //Return true if target point is within the specified field of view angle
    public bool CastFieldOfViewCone(Vector3 targetPoint)
    {
        point1 = (targetPoint - transform.position).normalized;
        point2 = transform.forward;
        float cosAngle = Vector3.Dot(point1, point2);
        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        return angle < fieldOfViewAngle;
    }

    public void OnDrawGizmos()
    {
        //Draw field of view
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawFrustum(transform.position, 6.5f, Camera.main.farClipPlane, Camera.main.nearClipPlane, 16.9f);
        
    }
}
