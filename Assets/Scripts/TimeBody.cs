using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBody : MonoBehaviour
{
    public float timer = 0;
    public float maxtimer = 5;
    public Slider reverseMeter; // UI slider to show rewind cooldown

    bool isRewinding = false;

    public float recordTime = 5f;

    List<PointInTime> pointsInTime; // Stores recorded positions and rotations

    Rigidbody2D rb;

    
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < maxtimer) // Increase timer until max
        {
            timer += Time.deltaTime;
            reverseMeter.value = timer; // Update UI slider
        }
        if (Input.GetKeyDown(KeyCode.F) && timer >= maxtimer) // Start rewind if timer is full
            StartRewind();
        if (Input.GetKeyUp(KeyCode.F)) // Stop rewind when key is released
            StopRewind();
    }

    void FixedUpdate()
    {
        if (isRewinding)
            Rewind(); // Perform rewind
        else
            Record(); // Record position and rotation
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0]; // Get the latest recorded point
            transform.position = pointInTime.position; // Restore position
            transform.rotation = pointInTime.rotation; // Restore rotation
            pointsInTime.RemoveAt(0); // Remove used point
        }
        else
        {
            StopRewind(); // Stop rewinding if no points left
        }
    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime)) // Limit record storage
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1); // Remove oldest record
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation)); // Store current state
    }

    public void StartRewind()
    {
        timer = 0; // Reset timer
        reverseMeter.value = timer; // Reset UI slider
        isRewinding = true;
        rb.isKinematic = true; // Disable physics during rewind
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false; // Re-enable physics after rewind
    }
}
