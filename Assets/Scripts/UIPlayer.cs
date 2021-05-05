using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayer : MonoBehaviour
{
    public void PlayHighlight() => AkSoundEngine.PostEvent("Play_Highlight", this.gameObject);
    public void PlaySelect() => AkSoundEngine.PostEvent("Play_Select", this.gameObject);
    public void PlayStart() => AkSoundEngine.PostEvent("Play_Start", this.gameObject);
    public void PlayExit() => AkSoundEngine.PostEvent("Play_Exit", this.gameObject);
    public void PlayBack() => AkSoundEngine.PostEvent("Play_Back", this.gameObject);
}
