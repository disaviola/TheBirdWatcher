﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Base class for all behavior common to all birds.
 * Bird type specific behavior (such as which trees can be landed on) can be added in the deriving child classes.
*/
public class BirdQ : MonoBehaviour
{
    private Animator animator;
    private float flightSpeed, observationTime;
    //[Tooltip("The flight speed of the bird.")]
    //[SerializeField] protected float flightSpeed = 5f;
    //[Tooltip("The amount of time (in seconds) the bird needs to be observed by the player before it is alerted.")]
    //[SerializeField] private float observationTime = 1.5f;

    //[SerializeField] protected BirdPropertyData birdPropertyData;

    #region Class behavior booleans
    private bool canBeDetected = true;
    private bool timerRunning = false;
    private bool flying = false;
    #endregion

    #region Landing points
    protected List<LandingPoint> landingPoints = new List<LandingPoint>();
    protected List<LandingPoint> nonVisiblePoints = new List<LandingPoint>();
    protected LandingPoint currentPoint, newPoint;
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        TestIfVisible();
        if (flying)
        {
            FlyToPoint();
        }
        UpdateNonVisiblePoints();       
    }

    //Tests if the bird is visible to the player by checking if the bird is within the FOV range, and able to be detected.
    //Birds can only be detected while on a landing point.
    private void TestIfVisible()
    {
		if (!ObjectDetecting.instance)
			return;

        if (canBeDetected && ObjectDetecting.instance.CastFieldOfViewCone(transform.position))
        {          
            if (!timerRunning)
            {
                Debug.Log("Bird Seen");
                StartCoroutine(WatchTimer());
            }              
        }
    }

    //Coroutine for timing the player watch/reaction time. 
    //The bird needs to be observed by the player for X amount of time before the bird is alerted.
    //If player is still observing bird X seconds after initial observation, the bird is alerted and seeks new landing point.
    private IEnumerator WatchTimer()
    {
        timerRunning = true;
        yield return new WaitForSeconds(observationTime);
        if (ObjectDetecting.instance.CastFieldOfViewCone(transform.position))
        {
            animator.SetTrigger("IsAlerted");
            canBeDetected = false;
            ChangeLandingPoint();
        }
        timerRunning = false;
    }

    //Selects random new landing point from list of non-visible points, making sure it is not a currently occupied one.
    private void ChangeLandingPoint()
    {       
        int index = Random.Range(0, nonVisiblePoints.Count);
        try { 
            newPoint = nonVisiblePoints[index];
        }
        //If no available non-visible trees, bird remains
        catch (System.ArgumentOutOfRangeException)
        {
            return;
        }
        if (newPoint.Equals(currentPoint) || newPoint.isOccupied || newPoint.Targeted)
        {
            try
            {
                newPoint = nonVisiblePoints[index + 1];
            }
            catch (System.ArgumentOutOfRangeException)
            {
                newPoint = nonVisiblePoints[index - 1];
            }
        }
        currentPoint.isOccupied = false;
        currentPoint.Occupator = null;
        flying = true;
        newPoint.Targeted = true;
        animator.SetBool("IsFlying", true);
        
    }

    //Moves bird to the chosen new point. 
    //Bird begins flight after the alert animation is finished and ends flight when within a short range of the landing point.
    //Once bird has landed, it can now be detected again.
    private void FlyToPoint()
    {     
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Flying"))
        {
            transform.LookAt(newPoint.transform.position);
            //transform.position = Vector3.Lerp(transform.position, newPoint.transform.position, flightSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, newPoint.transform.position, flightSpeed * Time.deltaTime);
        }
        
        if (Vector3.Distance(transform.position, newPoint.transform.position) < 2f)
        {
            flying = false;
            animator.SetBool("IsFlying", false);
            animator.SetTrigger("HasLanded");
            currentPoint = newPoint;
            currentPoint.isOccupied = true;
            currentPoint.Occupator = this.gameObject;
            currentPoint.Targeted = false;
            canBeDetected = true;
        }
    }

    //Draws flight path and landing point. 
    //Catches for case where new landing point has not been assigned, such as upon initialization.
    private void OnDrawGizmos()
    {
        try
        {            
            if (flying)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(currentPoint.transform.position, newPoint.transform.position);
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(newPoint.transform.position, 3f);
            }
        }
        catch (System.NullReferenceException)
        {

        }
    }

    //Virtual method to be overridden by deriving child classes.
    public virtual void AddLandingPoints()
    {

    }

    //Updates the list of currently non-visible landing points.
    protected void UpdateNonVisiblePoints()
    {
        foreach(LandingPoint point in landingPoints)
        {
            if (nonVisiblePoints.Contains(point))
            {
                if (point.TestIfVisible() == true)
                {
                    nonVisiblePoints.Remove(point);
                }
            }
            else if(nonVisiblePoints.Contains(point) == false)
            {
                if (point.TestIfVisible() == false)
                {
                    nonVisiblePoints.Add(point);
                }
            }
        }
    }

    //Finds a free landing point to spawn on
    public void FlyToSpawn()
    {
        foreach (LandingPoint point in landingPoints)
        {
            if (!point.isOccupied && !point.Targeted)
            {
                transform.position = point.transform.position;
                currentPoint = point;
                point.isOccupied = true;
                point.Occupator = this.gameObject;
                break;
            }
        }
    }

    public void SetProperties(float flightSpeed, float observationTime)
    {
            this.flightSpeed = flightSpeed;
            this.observationTime = observationTime;
    }

	public void SetBehaviourLandOnAnyTrees ()
	{
		LandingPoint[] points = FindObjectsOfType<LandingPoint>();

		foreach (LandingPoint point in points)
		{
			landingPoints.Add(point);
		}
		//Debug.Log("Setting Behaviour: Land On All Trees" + landingPoints.Count + " | " + gameObject.name);
	}

	public void SetBehaviourLandOnTreesWithLeaves()
	{
		LandingPoint[] points = FindObjectsOfType<LandingPoint>();

		foreach (LandingPoint point in points)
		{
			if (point.CompareTag("TreeWithLeaves"))
			{
				landingPoints.Add(point);
			}
		}
	}
}
