using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isCheckPoint;
    public int intesityValue;
    public int healAmount;
    public float newCameraPos;
    public string audioToPlay;
    public int playAudioInt;

    // Wwise - Switches
    [Header("Wwise Switch Names")]
    [SerializeField] private string dialogueLineSwitch_1 = "Checkpoint_0";
    [SerializeField] private string dialogueLineSwitch_2 = "Checkpoint_1";
    [SerializeField] private string dialogueLineSwitch_3 = "Checkpoint_2";
    [SerializeField] private string dialogueLineSwitch_4 = "Checkpoint_3";
    [SerializeField] private string dialogueLineSwitch_5 = "BossFight";
    [SerializeField] private string dialogueLineSwitch_6 = "BossDefeated";

    // Step 1: Declare the necessary fields
    [SerializeField] private string dialogueEventName = "Play_Dialogue";
    private uint dialogueEventID;
    private uint dialogueSequenceID;
    private AkPlaylist akPlaylist;

    // Step 2: Define the IDs (this can be done in a separate function)
    private void Awake()
    {
        dialogueEventID = AkSoundEngine.GetIDFromString(dialogueEventName);
        dialogueSequenceID = AkSoundEngine.DynamicSequenceOpen(gameObject);
    }

    // Step 3: Create a method that sets Switches/States and posts a Dynamic Dialogue Event
    private void PlayDynamicDialogue(uint switchID_0, uint switchID_1, uint switchID_2)
    {
        // Step A: Create a uint container and populate it with Switch and/or State IDs
        uint[] dialoguePath =
        {
            switchID_0,
            switchID_1,
            switchID_2
        };

        // Step B: Generate an Audio Node and get its ID
        /*  Arguments:
         *      Dynamic Dialogue Event ID (uint)  *
         *      Desired Switch/State IDs (uint[]) *
         *      Number of Switches/States (uint)  */
        uint dialogueNodeID = AkSoundEngine.ResolveDialogueEvent
        (
            dialogueEventID,
            dialoguePath,
            (uint)dialoguePath.Length
        );

        // Step C: Lock the Playlist
        //  Argument: Dynamic Dialogue Sequence ID (uint)
        akPlaylist = AkSoundEngine.DynamicSequenceLockPlaylist(dialogueSequenceID);

        // Step D: Enqueue the Audio Node
        //  Argument: Audio Node ID (uint)
        akPlaylist.Enqueue(dialogueNodeID);

        // Step E: Unlock the Playlist
        //  Argument: Dynamic Dialogue Sequence ID (uint)
        AkSoundEngine.DynamicSequenceUnlockPlaylist(dialogueSequenceID);

        // Step F: Play the Sequence
        //  Argument: Dynamic Dialogue Sequence ID (uint)
        AkSoundEngine.DynamicSequencePlay(dialogueSequenceID);

        // Step G: Close the Sequence
        //  Argument: Dynamic Dialogue Sequence ID (uint)
        AkSoundEngine.DynamicSequenceClose(dialogueSequenceID);
    }

    public void PlayDialogue(uint switchID)
    {
        // Step 1: Create a container and populate it with Switch and/or State IDs
        uint[] dialoguePath = { switchID };

        // Step 2: Generate a Node and get its ID
        uint dialogueNodeID = AkSoundEngine.ResolveDialogueEvent(dialogueEventID, dialoguePath, 1);

        // Step 3: Lock the Playlist
        akPlaylist = AkSoundEngine.DynamicSequenceLockPlaylist(dialogueSequenceID);

        // Step 4: Enqueue the Node
        akPlaylist.Enqueue(dialogueNodeID);

        // Step 5: Unlock the Playlist
        AkSoundEngine.DynamicSequenceUnlockPlaylist(dialogueSequenceID);

        // Step 6: Play the Sequence
        AkSoundEngine.DynamicSequencePlay(dialogueSequenceID);

        // Step 7: Close the Sequence
        AkSoundEngine.DynamicSequenceClose(dialogueSequenceID);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Hitbox")
        {
            if (isCheckPoint)
            {
                LevelControl.instance.ChangeIntensity(intesityValue);
                other.GetComponent<HealthControl>().respawnArea.position = this.transform.position;
                other.GetComponent<HealthControl>().Heal(healAmount);
            }

            else
            {
                switch (playAudioInt)
                {
                    case 1: 
                        PlayDialogue(AkSoundEngine.GetIDFromString(dialogueLineSwitch_1));  // Checkpoint_0
                        break;
                    case 2: 
                        PlayDialogue(AkSoundEngine.GetIDFromString(dialogueLineSwitch_2));  // Checkpoint_1
                        break;
                    case 3: 
                        PlayDialogue(AkSoundEngine.GetIDFromString(dialogueLineSwitch_3));  // Checkpoint_2
                        break;
                    case 4: 
                        PlayDialogue(AkSoundEngine.GetIDFromString(dialogueLineSwitch_4));  // Checkpoint_3
                        break;
                    case 5: 
                        PlayDialogue(AkSoundEngine.GetIDFromString(dialogueLineSwitch_5));  // BossFight
                        AkSoundEngine.SetState("GameState", "BossFight");
                        AkSoundEngine.SetState("BossState", "Alive");
                        break;
                    default: 
                        Debug.Log("No dialogue line was played."); 
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
