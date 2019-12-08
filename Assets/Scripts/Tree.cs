using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public bool TestIfVisible()
    {
        if (BirdDetecting.instance.CastFieldOfViewCone(transform.position))
        {
            return true;
        }
        return false;
    }
}
