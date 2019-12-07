using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Animator animator;

    private bool canBeDetected = true;
    private bool timerRunning = false;
    private bool flying = false;

    [SerializeField] protected float flightSpeed = 5f;

    protected List<LandingPoint> landingPoints = new List<LandingPoint>();
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
        Debug.Log(newPoint.transform.position);
        currentPoint.isOccupied = false;
        flying = true;
        animator.SetBool("IsFlying", true);
        
    }

    private void FlyToPoint()
    {
        //this.transform.position += newPoint.transform.position * flightSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, newPoint.transform.position, flightSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, newPoint.transform.position) < 2f)
        {
            flying = false;
            animator.SetBool("IsFlying", false);
            animator.SetBool("HasLanded", true);
            currentPoint = newPoint;
            currentPoint.isOccupied = true;
            canBeDetected = true;
        }
    }

    private void OnDrawGizmos()
    {
        try
        {
            Gizmos.DrawSphere(newPoint.transform.position, 0.1f);
        }
        catch (System.NullReferenceException)
        {

        }
    }


    public virtual void AddLandingPoints()
    {

    }
}
