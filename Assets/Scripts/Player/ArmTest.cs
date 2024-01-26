using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmTest : MonoBehaviour
{
    public Rigidbody2D character;
    public Rigidbody2D hammer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public float maxDistance = 3f;

    void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure the z-coordinate is 0 in a 2D project

        // Calculate the direction from the character to the mouse
        Vector3 direction = mousePosition - character.transform.position;

        // Limit the distance of the hammer from the character
        if (direction.magnitude > maxDistance)
        {
            direction = direction.normalized * maxDistance;
        }

        // Set the position of the hammer
        hammer.transform.position = character.transform.position + direction;

        // Rotate the hammer to face the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        hammer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        // Check if the collision is with another object
        if (collision.collider.gameObject.CompareTag("Obstacle"))
        {
            // Access the contact points to calculate the force
            ContactPoint2D[] contactPoints = collision.contacts;

            foreach (ContactPoint2D contact in contactPoints)
            {
                // Calculate the force vector
                Vector2 force = contact.normal * collision.relativeVelocity.magnitude;

                // Apply the force to your character
                character.AddForce(force*0.5f, ForceMode2D.Impulse);
            }
        }
    }
}
