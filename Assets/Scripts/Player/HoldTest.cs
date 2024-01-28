using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldTest : MonoBehaviour
{
    public GameState game_state;
    public GameObject character;
    public float RaycastDistance = 1;
    public LayerMask rayMask;

    private Rigidbody2D other;
    public SpringJoint2D SpringJoint;
    public float force;
    

    private void Update()
    {
        SpringJoint.enabled = other;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other)
            {
                // Fire
                SpringJoint.connectedBody = null;
                // holding.gameObject.GetComponent<Rigidbody2D>().simulated = true;
                SpringJoint.enabled = false;
                other.AddForce(HammerTest.hingeDirection * force, ForceMode2D.Impulse);
                other.gravityScale *= 10f;
                other = null;
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(character.transform.position, HammerTest.hingeDirection, RaycastDistance, rayMask);
                other = hit.collider.GetComponent<Rigidbody2D>();
                if (other && other.gameObject.layer == 7)
                {
                    Debug.Log("Holdable");

                    SpringJoint.connectedBody = other;
                    SpringJoint.connectedAnchor = Vector2.zero;
                    other.gravityScale *= 0.1f;
                    SpringJoint.enabled = true;
                    // holding.gameObject.GetComponent<Rigidbody2D>().simulated = false;
                }
            }
            
            
        }

        // if (holding && holding_timer < 1)
        // {
        //     holding_timer += Time.deltaTime;
        //     if (holding_timer > 1)
        //     {
        //         holding_timer = 1;
        //     }
        // }
    }
}