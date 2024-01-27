using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameState game_state;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Apple")
        {
            game_state.ScoreIncrease(1);
            Destroy(collision.gameObject);
        }
    }
}
