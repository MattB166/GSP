using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{

    [SerializeField] private GameObject[] WayPoints; //creates an array of waypoints to use as a path to follow 
    private int CurrentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;


    private void FixedUpdate()
    {
        if (Vector2.Distance(WayPoints[CurrentWaypointIndex].transform.position,transform.position)< .1f) // if current waypoint has been hit, move to next waypoint
        {
            CurrentWaypointIndex++;
            if(CurrentWaypointIndex >= WayPoints.Length)
            {
                CurrentWaypointIndex = 0; //back to first way point if all waypoints been reached
            }
        }
        //moving towards the targeted waypoint 
        transform.position = Vector2.MoveTowards(transform.position, WayPoints[CurrentWaypointIndex].transform.position, Time.deltaTime * speed); //sets travel 
    }
}
