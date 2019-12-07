using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_LandOnAnyTrees : Bird
{
    private void Start()
    {
        AddLandingPoints();
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
