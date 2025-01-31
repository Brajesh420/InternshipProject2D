using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    void Update()
    {
         transform.position = new Vector3(Player.position.x,Player.position.y,transform.position.z);
        //transform.position = Player.position;
    }
}
