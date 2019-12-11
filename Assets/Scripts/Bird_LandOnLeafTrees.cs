using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_LandOnLeafTrees : Bird
{

    public override void AddLandingPoints()
    {

        LandingPoint[] points = FindObjectsOfType<LandingPoint>();
        
        foreach(LandingPoint point in points)
        {
            if (point.CompareTag("TreeWithLeaves"))
            {
                landingPoints.Add(point);
            }
        }
    }
}
