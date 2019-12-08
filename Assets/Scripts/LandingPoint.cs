﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPoint : MonoBehaviour
{
    private bool occupied = false;

    //Gets and sets the current occupation status for this landing point.
    //Returns true if the point is occupied by any bird.
    public bool isOccupied
    {
        get { return occupied; }
        set { occupied = value; }
    }

    //Tests if the landing point is visible to the player by checking if it is within the FOV range.
    //Returns true if landing point is currently visible to the player.
    public bool TestIfVisible()
    {
        if (BirdDetecting.instance.CastFieldOfViewCone(transform.position))
        {
            return true;    
        }
        return false;
    }

}
