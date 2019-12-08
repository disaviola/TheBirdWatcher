﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPoint : MonoBehaviour
{
    private bool occupied = false;
    public bool isOccupied
    {
        get { return occupied; }
        set { occupied = value; }
    }

    public bool TestIfVisible()
    {
        if (BirdDetecting.instance.CastFieldOfViewCone(transform.position))
        {
            return true;
     
        }
        return false;
    }

}
