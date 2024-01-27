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
    public LayerMask rayMask;
    public float raycastDistance = 1f;
    private bool isGrounded;

    private Vector3 mouseDirection;
    private Vector3 hingeDirection;

    private void Start()
    {
        hinge = character.GetComponent<HingeJoint2D>();
        motor = hinge.motor;
        motor.maxMotorTorque = 500;
        hinge.useMotor = true;
        
        // TODO: Optimize this
        character.centerOfMass = new Vector2(0, -1);
        
        pid = new PID(5e-3f, 1e-3f, 1e-2f);
    }

    void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Calculate the direction from the character to the mouse
        mouseDirection = (mousePosition - character.transform.position).normalized;
        
        hingeDirection = (Quaternion.Euler(
            hammer.transform.eulerAngles) * Vector3.down).normalized;

        var angle = GetAngle(mouseDirection, hingeDirection);
        SetSpeed(-Mathf.Atan(angle)*1000);
        
        UpdateRotateCount();
        
        // Check for player input to punch an enemy
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time - lastPunchTime > punchCooldown)
        {
            Punch();
            lastPunchTime = Time.time;
        }
    }

    private void FixedUpdate()
    {
        var current = character.transform.eulerAngles.z - rotateCount * 360;
        
        var torque = pid.Update(0, current, 0.02f);
        if (torque > 10f)
        {
            Debug.Log(torque);
        }
        
        character.AddTorque(torque, ForceMode2D.Impulse);
        
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
        RaycastHit2D hit = Physics2D.Raycast(character.transform.position, Vector2.down, raycastDistance);
        if (hit.collider)
        {
            isGrounded = hit.collider.CompareTag("Ground");
        }
    }
    
    public float punchForce = 50f;
    public float punchCooldown = 1f;
    public float punchGroundForce = 50f;
    
    [Tooltip("Send your enemy flying")]
    public float punchKnockUp = 20f;
    public float punchDist = 1f;

    private float lastPunchTime;

    void Punch()
    {
        RaycastHit2D hit = Physics2D.Raycast(hammer.transform.position, hingeDirection, punchDist, rayMask);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("Enemy"))
            {
                // Apply force to the enemy
                Rigidbody2D enemyRigidbody = hit.collider.GetComponent<Rigidbody2D>();
                if (enemyRigidbody)
                {
                    
                    enemyRigidbody.AddForce(hingeDirection * punchForce + Vector3.up * punchKnockUp, ForceMode2D.Impulse);
                }

                // Apply force to the character (knock back)
                character.GetComponent<Rigidbody2D>().AddForce(-hingeDirection * punchForce, ForceMode2D.Impulse);
            }
            else if (hit.collider.CompareTag("Ground"))
            {
                character.GetComponent<Rigidbody2D>().AddForce(-hingeDirection * punchGroundForce, ForceMode2D.Impulse);
            }
        }
    }
}

