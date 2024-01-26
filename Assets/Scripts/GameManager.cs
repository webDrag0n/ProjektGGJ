using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Stopped,
    Paused,
    Playing
}

public class GameManager : MonoBehaviour
{
    public GameState state;
    public GameObject player;
    public float score;

    // Start is called before the first frame update
    void Start()
    {
        //state = GameState.Stopped;
        //score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
