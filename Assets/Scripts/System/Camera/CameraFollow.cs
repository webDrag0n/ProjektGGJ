using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float x_wall_positive;
    public float x_wall_negative;
    public float y_wall_positive;
    public float y_wall_negative;

    // Update is called once per frame
    void Update()
    {
        if (target.position.x < x_wall_positive
            && target.position.x > x_wall_negative
            && target.position.y < y_wall_positive
            && target.position.y > y_wall_negative
            )
        {
            transform.position = Vector3.Lerp(transform.position, target.position + new Vector3(0,0,-10), 5*Time.deltaTime);
        }
    }
}
