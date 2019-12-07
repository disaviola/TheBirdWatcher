using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Animator animator;

    private bool timerRunning = false;

    protected List<LandingPoint> landingPoints;
    private LandingPoint currentPoint, newPoint;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void TestIfVisible()
    {
        if (BirdDetecting.instance.CastFieldOfViewCone(transform.position))
        {          
            if (!timerRunning)
            {
                Debug.Log("Bird Seen");
                StartCoroutine(WatchTimer());
            }              
        }
    }

    private void Update()
    {
        TestIfVisible();
    }

    private IEnumerator WatchTimer()
    {
        timerRunning = true;
        yield return new WaitForSeconds(1.5f);
        if (BirdDetecting.instance.CastFieldOfViewCone(transform.position))
        {
            animator.SetTrigger("IsAlerted");
        }
        timerRunning = false;
    }

    private void ChangeLandingPoint()
    {
        //Select random new landing point, making sure it is not the currently occupied one
        int index = Random.Range(0, landingPoints.Count);
        newPoint = landingPoints[index];
        if (newPoint.Equals(currentPoint) || newPoint.isOccupied)
        {
            try
            {
                newPoint = landingPoints[index + 1];
            }
            catch (System.IndexOutOfRangeException)
            {
                newPoint = landingPoints[index - 1];
            }
        }
        FlyToPoint();
        currentPoint = newPoint;
    }

    private void FlyToPoint()
    {

        transform.position += newPoint.transform.position * Time.deltaTime;
    }


    public virtual void AddLandingPoints()
    {

    }
}
