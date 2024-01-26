using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadCollide : MonoBehaviour
{
    public GameManager manager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("dead");
        manager.GameOver();
    }
}
