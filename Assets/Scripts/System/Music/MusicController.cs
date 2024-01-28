using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public GameState game_state;
    private AudioSource audio_source;

    // Start is called before the first frame update
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
        audio_source.volume = 1.0f;
        audio_source.pitch = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        audio_source.volume = game_state.music_volume;
        audio_source.pitch = game_state.music_speed;
    }
}
