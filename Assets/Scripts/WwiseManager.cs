using UnityEngine;

public class WwiseManager : MonoBehaviour
{
    // Load bank (by string, since the reference was sometimes crashing due to v2017 incompatibility).
    private void Awake() => AkBankManager.LoadBank("Main", false, false);
}
