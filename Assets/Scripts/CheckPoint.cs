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

    // Necessary fields
    public static string dialogueEventName = "Play_Dialogue";
    private uint dialogueEventID;
    private uint dialogueSequenceID;
    private AkPlaylist akPlaylist;

    // Define the IDs (this can be done in a separate function)
    private void Awake()
    {
        dialogueEventID = AkSoundEngine.GetIDFromString(dialogueEventName);
        dialogueSequenceID = AkSoundEngine.DynamicSequenceOpen(this.gameObject, (uint)AkCallbackType.AK_EndOfDynamicSequenceItem, OnDialogueLineFinished, null);
    }

    private void OnDialogueLineFinished(object cookie, AkCallbackType type, AkCallbackInfo info)
    {
        // Add any additional functionality here (to be run every time a dialogue line finishes playing).
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

    // Stop any dialogue by playing silence
    public static void StopAllDialogue(GameObject gameObjectReference)
    {
		uint dialogueEventID = AkSoundEngine.GetIDFromString(CheckPoint.dialogueEventName);
        uint dialogueSequenceID = AkSoundEngine.DynamicSequenceOpen(gameObjectReference);
        uint dialogueNodeID = AkSoundEngine.ResolveDialogueEvent(dialogueEventID, new uint[] {AkSoundEngine.GetIDFromString("Silence")}, 1);
        AkSoundEngine.DynamicSequenceLockPlaylist(dialogueSequenceID).Enqueue(dialogueNodeID);;
        AkSoundEngine.DynamicSequenceUnlockPlaylist(dialogueSequenceID);
        AkSoundEngine.DynamicSequencePlay(dialogueSequenceID);
        AkSoundEngine.DynamicSequenceClose(dialogueSequenceID);
		AkSoundEngine.SetState("DialogueState", "NotPlaying");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.enabled == true && other.name == "Hitbox")
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
