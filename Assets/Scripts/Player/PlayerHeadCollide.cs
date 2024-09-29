using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadCollide : MonoBehaviour
{
    public Transform player;
    public float headHitThreshold = 20f; // Adjust this threshold as needed
    public AudioSource hit_audio;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Calculate the relative velocity of the collision
        float collisionSpeed = collision.relativeVelocity.magnitude;
        
        //Debug.Log("Collision Speed:" + collisionSpeed);
        // Check if the collision speed is above the threshold or is a sword
        if (collisionSpeed > headHitThreshold || collision.transform.tag == "Sword")
        {
            Dead();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Sword")
        {
            Dead();
        }
    }

    private void Dead()
    {
        player.gameObject.SetActive(false);
        Debug.Log("Character died!");
        hit_audio.Play();
        
        player.transform.position = Vector3.zero;
        player.transform.rotation = Quaternion.identity;
        player.gameObject.SetActive(true);
        // TODO: Death logic
        // manager.GameOver();
    }
}
