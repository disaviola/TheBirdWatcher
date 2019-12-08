using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float dragSpeed = 3.5f;
    private float xAxis;
    private float yAxis;

    [Header("Invert Camera Axes")]
    [SerializeField] private bool invertX = false;
    [SerializeField] private bool invertY = false;

    //Moves the camera upon click and drag of left mouse button. 
    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            float x = Input.GetAxis("Mouse X") * dragSpeed;
            float y = -Input.GetAxis("Mouse Y") * dragSpeed;
            if (invertX)
            {
                x = -Input.GetAxis("Mouse X") * dragSpeed;
            }
            if (invertY)
            {
                y = Input.GetAxis("Mouse Y") * dragSpeed;
            }
            transform.Rotate(new Vector3(y, x, 0));
            xAxis = transform.rotation.eulerAngles.x;
            yAxis = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(xAxis, yAxis, 0);
        }
    }


}
