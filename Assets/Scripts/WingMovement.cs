using UnityEngine;

public class WingMovement : MonoBehaviour
{
    public void OnWingFlap()
    {
        if (this.gameObject.name == "Mecha Wings_0")
        {
            AkSoundEngine.PostEvent("Play_Enemy_Wings", this.gameObject);
        }

        else if (this.gameObject.name == "TheBaws" || this.gameObject.name == "boss_horse_wing")
        {
            AkSoundEngine.PostEvent("Play_Boss_Wings", this.gameObject);
        }
    }
}
