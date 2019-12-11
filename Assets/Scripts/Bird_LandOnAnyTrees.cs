using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_LandOnAnyTrees : Bird
{
    public override void AddLandingPoints()
    {

        LandingPoint[] points = FindObjectsOfType<LandingPoint>();

        foreach (LandingPoint point in points)
        {
            landingPoints.Add(point); 
        }
        Debug.Log("Alltrees" + landingPoints.Count);
    }
}
