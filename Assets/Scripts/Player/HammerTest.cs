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
    private int rotateCount;
    private float lastZ;
    private PID pid;

    // TODO: Do we need a new layer?
    public LayerMask groundMask;
    public float raycastDistance = 1f;
    private bool isGrounded;

    private void Start()
    {
        hinge = character.GetComponent<HingeJoint2D>();
        motor = hinge.motor;
        motor.maxMotorTorque = 500;
        hinge.useMotor = true;
        
        // TODO: Optimize this
        character.centerOfMass = new Vector2(0, -1);
        
        // pid = new PID(1e-3f, -1e-3f, 1e-2f);
    }

    void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Calculate the direction from the character to the mouse
        Vector3 mouseDirection = (mousePosition - character.transform.position).normalized;
        
        Vector3 hingeDirection = (Quaternion.Euler(
            hammer.transform.eulerAngles) * Vector3.down).normalized;

        var angle = GetAngle(mouseDirection, hingeDirection);
        Debug.Log(angle);
        SetSpeed(-Mathf.Atan(angle)*500);
        
        UpdateRotateCount();
    }

    private void FixedUpdate()
    {
        var current = character.transform.eulerAngles.z - rotateCount * 360;
        
        // var torque = pid.Update(0, current, 0.02f);
        var torque = current;
            // Debug.Log(current);
        if (torque > 20f)
        {
            // Debug.Log(torque);
        }
        
        // character.AddTorque(torque, ForceMode2D.Impulse);
        
        
        UpdateGrounded();
        
        if (isGrounded)
        {
            if(Input.GetKey(KeyCode.D))
            {
                character.AddForce(Vector2.right, ForceMode2D.Impulse);
            }
            else if(Input.GetKey(KeyCode.A))
            {
                character.AddForce(Vector2.left, ForceMode2D.Impulse);
            }
        }
        
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

    // Keep track of rotation
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

    private void UpdateGrounded()
    {
        // Perform a raycast downwards
        RaycastHit2D hit = Physics2D.Raycast(character.transform.position, Vector2.down, raycastDistance, groundMask);

        isGrounded = hit.collider != null;
    }
    
}
