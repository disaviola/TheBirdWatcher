using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPoint : MonoBehaviour
{
    private bool occupied = false;
    private bool beingTargeted = false;
    private GameObject occupator;

    //Gets and sets the current occupation status for this landing point.
    //Returns true if the point is occupied by any bird.
    public bool isOccupied
    {
        get { return occupied; }
        set { occupied = value; }
    }

    public bool Targeted
    {
        get { return beingTargeted; }
        set { beingTargeted = value; }
    }

    public GameObject Occupator
    {
        get { return occupator; }
        set { occupator = value; }
    }

    //Tests if the landing point is visible to the player by checking if it is within the FOV range.
    //Returns true if landing point is currently visible to the player.
    public bool TestIfVisible()
    {
        if (ObjectDetecting.instance.CastFieldOfViewCone(transform.position))
        {
            return true;    
        }
        return false;
    }

}
