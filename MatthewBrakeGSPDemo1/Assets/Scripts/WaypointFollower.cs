using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{

    [SerializeField] private GameObject[] WayPoints;
    private int CurrentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;


    private void FixedUpdate()
    {
        if (Vector2.Distance(WayPoints[CurrentWaypointIndex].transform.position,transform.position)< .1f)
        {
            CurrentWaypointIndex++;
            if(CurrentWaypointIndex >= WayPoints.Length)
            {
                CurrentWaypointIndex = 0;
            }
        }
        //moving towards the targeted waypoint 
        transform.position = Vector2.MoveTowards(transform.position, WayPoints[CurrentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
