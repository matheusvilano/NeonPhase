using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Removed AK.Wwise.Event due to serialization problems with v2017.
    [Header("Wwise")]
    [SerializeField] private string music = "Play_Music";
    [SerializeField] private string initialState = "GameState/MainMenu";

    private bool isPlaying = false;

    private void Start()
    {

        // Check if the music is already playing; it not, play.
        if (!isPlaying)
        {
            AkSoundEngine.PostEvent(music, this.gameObject);
            this.isPlaying = true;
        }

        // Set initial Wwise State for Music
        AkSoundEngine.SetState(initialState.Split(new[]{'/'})[0], initialState.Split(new[]{'/'})[1]);
    }
}
