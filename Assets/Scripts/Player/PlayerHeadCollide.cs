using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadCollide : MonoBehaviour
{
    
    public float headHitThreshold = 20f; // Adjust this threshold as needed

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Calculate the relative velocity of the collision
        float collisionSpeed = collision.relativeVelocity.magnitude;
        
        Debug.Log("Collision Speed:" + collisionSpeed);
        // Check if the collision speed is above the threshold
        if (collisionSpeed > headHitThreshold)
        {
            Debug.Log("Character died!");
            transform.position = Vector3.zero; transform.rotation = Quaternion.identity;
            // TODO: Death logic
            // manager.GameOver();
        }
        
    }
}
