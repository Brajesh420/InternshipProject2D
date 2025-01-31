using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spritemask : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] SpriteMask mask;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
           Vector3 mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 0;

        if (Input.GetMouseButton(0))
        {
            mask.transform.position = mousepos;
            mask.enabled = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mask.transform.position = Input.mousePosition;
            mask.enabled = false;
        }
    }
}
