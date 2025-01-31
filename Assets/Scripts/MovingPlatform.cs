using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject[] WayPoint; // Array of waypoints for the platform to move between
    int CurrentIndex = 0; // Tracks the current waypoint index

    public float speed = 2f; 

    void Update()
    {
        
        if (Vector2.Distance(WayPoint[CurrentIndex].transform.position, transform.position) < 0.1f) // Check if platform is close to the current waypoint
        {
            CurrentIndex++; // Move to the next waypoint
            if (CurrentIndex >= WayPoint.Length) // If end of waypoints list is reached
            {
                CurrentIndex = 0; // Reset to the first waypoint
            }
        }

        
        transform.position = Vector2.MoveTowards(transform.position, WayPoint[CurrentIndex].transform.position, speed * Time.deltaTime); // Move platform toward the current waypoint
    }
}
