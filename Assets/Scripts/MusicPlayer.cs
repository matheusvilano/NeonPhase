using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event music = default;
    [SerializeField]
    private AK.Wwise.State initialState = default;

    private bool isPlaying = false;

    private void Start()
    {
        if (!isPlaying)
        {
            music.Post(this.gameObject);
            isPlaying = true;
        }
        initialState.SetValue();
    }
}
