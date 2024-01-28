using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState game_state;
    public Transform Win_UI;

    // Start is called before the first frame update
    void Start()
    {
        //state = GameState.Stopped;
        //score = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (game_state.GetScore() >= 4)
        {
            //game_state.Reset();
            //NextLevel();
            Win_UI.gameObject.SetActive(true);

        }
    }

    void NextLevel()
    {

    }

    public void GameOver()
    {
        game_state.Reset();
    }
}
