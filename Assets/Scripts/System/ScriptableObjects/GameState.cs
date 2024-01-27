using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    Stopped,
    Paused,
    Playing
}
[CreateAssetMenu(fileName = "GameStatus", menuName = "GameStatus", order = 1)]
public class GameState : ScriptableObject
{
    public GameStatus status;
    private float score;

    public void Reset()
    {
        status = GameStatus.Stopped;
        score = 0;
    }

    public void ScoreIncrease(float _score)
    {
        score += _score;
    }

    public float GetScore() {  return score; }

}
