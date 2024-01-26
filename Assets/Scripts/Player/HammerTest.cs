using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HammerTest : MonoBehaviour
{
    public Rigidbody2D hammer;
    public Rigidbody2D character;
    private HingeJoint2D hinge;
    private JointMotor2D motor;
    private float mouse;
    private int rotateCount;
    private float lastZ;
    private PID pid;

    private void Start()
    {
        hinge = character.GetComponent<HingeJoint2D>();
        motor = hinge.motor;
        motor.maxMotorTorque = 500;
        hinge.useMotor = true;
        character.centerOfMass = new Vector2(0, -1);
        pid = new PID(5e-2f, -1e-3f, 1e-2f);
    }

    void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure the z-coordinate is 0 in a 2D project

        // Calculate the direction from the character to the mouse
        Vector3 mouseDirection = (mousePosition - character.transform.position).normalized;
        
        Vector3 hingeDirection = (Quaternion.Euler(
            hammer.transform.eulerAngles) * Vector3.down).normalized;

        var angle = GetAngle(mouseDirection, hingeDirection);
        SetSpeed(-Mathf.Atan(angle)*1000);
        
        UpdateRotateCount();
    }

    private void FixedUpdate()
    {
        var current = character.transform.eulerAngles.z - rotateCount * 360;
        Debug.Log(current);
        var torque = pid.Update(0, current, 0.02f);
        character.AddTorque(torque, ForceMode2D.Impulse);
    }

    // Get the min angle in rads that can rotate the first vector to the second
    float GetAngle(Vector3 v1, Vector3 v2)
    {
        float sign = Mathf.Sign(Vector3.Cross(v1.normalized, v2.normalized).z);
        float angle = Mathf.Acos(Vector3.Dot(v1.normalized, v2.normalized));
        
        return sign*angle;
    }

    void SetSpeed(float speed)
    {
        motor.motorSpeed = speed;
        hinge.motor = motor;
    }

    void UpdateRotateCount()
    {
        if (lastZ>=340&&character.transform.eulerAngles.z<=20)
        {
            rotateCount--;
        }
        else if (lastZ<=20&&character.transform.eulerAngles.z>=340)
        {
            rotateCount++;
        }
        lastZ = character.transform.eulerAngles.z;
    }
    
}
