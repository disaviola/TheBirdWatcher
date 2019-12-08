using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for common bird behavior, bird type specific behavior (such as which trees can be landed on) can be added in deriving child classes
public class Bird : MonoBehaviour
{
    private Animator animator;

    private bool canBeDetected = true;
    private bool timerRunning = false;
    private bool flying = false;

    [SerializeField] protected float flightSpeed = 5f;

    protected List<LandingPoint> landingPoints = new List<LandingPoint>();
    protected List<LandingPoint> nonVisiblePoints = new List<LandingPoint>();
    protected LandingPoint currentPoint, newPoint;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void TestIfVisible()
    {
        if (canBeDetected && BirdDetecting.instance.CastFieldOfViewCone(transform.position))
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
        if (flying)
        {
            FlyToPoint();           
        }
        UpdateNonVisiblePoints();
    }

    private IEnumerator WatchTimer()
    {
        timerRunning = true;
        yield return new WaitForSeconds(1.5f);
        if (BirdDetecting.instance.CastFieldOfViewCone(transform.position))
        {
            animator.SetTrigger("IsAlerted");
            canBeDetected = false;
            ChangeLandingPoint();
        }
        timerRunning = false;
    }

    private void ChangeLandingPoint()
    {
        //Select random new landing point, making sure it is not the currently occupied one
        int index = Random.Range(0, nonVisiblePoints.Count);
        newPoint = nonVisiblePoints[index];
        if (newPoint.Equals(currentPoint) || newPoint.isOccupied)
        {
            try
            {
                newPoint = nonVisiblePoints[index + 1];
            }
            catch (System.IndexOutOfRangeException)
            {
                newPoint = nonVisiblePoints[index - 1];
            }
        }
        Debug.Log(newPoint.transform.position);
        currentPoint.isOccupied = false;
        flying = true;
        animator.SetBool("IsFlying", true);
        
    }

    private void FlyToPoint()
    {
        //this.transform.position += newPoint.transform.position * flightSpeed * Time.deltaTime;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Flying"))
        {
            transform.position = Vector3.Lerp(transform.position, newPoint.transform.position, flightSpeed * Time.deltaTime);
        }
        
        if (Vector3.Distance(transform.position, newPoint.transform.position) < 2f)
        {
            flying = false;
            animator.SetBool("IsFlying", false);
            animator.SetTrigger("HasLanded");
            currentPoint = newPoint;
            currentPoint.isOccupied = true;
            canBeDetected = true;
        }
    }

    private void OnDrawGizmos()
    {
        try
        {            
            if (flying)
            {
                //Draw flight path and landing point
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


    public virtual void AddLandingPoints()
    {

    }

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

    public void UpdatePoints(LandingPoint point, bool remove)
    {
        if (remove && nonVisiblePoints.Contains(point))
        {
            nonVisiblePoints.Remove(point);
        }
        else if(!remove && !nonVisiblePoints.Contains(point))
        {
            nonVisiblePoints.Add(point);
        }
    }
}
