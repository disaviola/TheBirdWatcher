using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_LandOnAnyTrees : Bird
{
    private void Start()
    {
        AddLandingPoints();
        transform.position = landingPoints[0].transform.position;
        currentPoint = landingPoints[0];
    }


    public override void AddLandingPoints()
    {
        LandingPoint[] points = FindObjectsOfType<LandingPoint>();

        foreach (LandingPoint point in points)
        {
            landingPoints.Add(point); 
        }
    }
}
