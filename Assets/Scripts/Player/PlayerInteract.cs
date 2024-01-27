using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameState game_state;
    public Transform holding;
    public float holding_timer = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Apple")
        {
            game_state.ScoreIncrease(1);
            Destroy(collision.gameObject);
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (collision.gameObject.layer == 7)
            {
                Debug.Log("Holdable");
                if (!holding)
                {
                    holding = collision.transform;
                    holding.parent = transform;
                    holding.gameObject.GetComponent<Rigidbody2D>().simulated = false;
                    
                }
                
            }
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && holding && holding_timer == 1)
        {
            // Fire
            holding.transform.parent = null;
            holding.gameObject.GetComponent<Rigidbody2D>().simulated = true;
            holding.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * 300, ForceMode2D.Impulse);
            holding = null;
            holding_timer = 0;

            
        }

        if (holding && holding_timer < 1)
        {
            holding_timer += Time.deltaTime;
            if (holding_timer > 1)
            {
                holding_timer = 1;
            }
        }
    }
}
