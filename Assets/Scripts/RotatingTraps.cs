using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTraps : MonoBehaviour
{
    public float Speed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 360 * Speed * Time.deltaTime);
    }
}
