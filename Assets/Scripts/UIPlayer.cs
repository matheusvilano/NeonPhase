using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayer : MonoBehaviour
{
    // Predefined Wwise Event Names (they will NOT change, do not expose on the Inspector)
    private const string playHighlight = "Play_Highlight";
    private const string playSelect = "Play_Select";
    private const string playStart = "Play_Start";
    private const string playExit = "Play_Exit";
    private const string playBack = "Play_Back";

    // Function wrapper definitions (marked public so that we can setup callbacks for UI)
    public void PlayHighlight() => AkSoundEngine.PostEvent(UIPlayer.playHighlight, this.gameObject);
    public void PlaySelect() => AkSoundEngine.PostEvent(UIPlayer.playSelect, this.gameObject);
    public void PlayStart() => AkSoundEngine.PostEvent(UIPlayer.playStart, this.gameObject);
    public void PlayExit() => AkSoundEngine.PostEvent(UIPlayer.playExit, this.gameObject);
    public void PlayBack() => AkSoundEngine.PostEvent(UIPlayer.playBack, this.gameObject);
}
