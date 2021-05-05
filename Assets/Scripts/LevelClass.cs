using UnityEngine;

public class LevelClass : MonoBehaviour 
{
	public int numberOfEnemies; // the number of enemies in the level
	
	public bool isStarted; // did the player start the level
	public bool isEnded; // did the player beat the level
	public bool isPaused; // is the game paused

	public int playersHealth; // the health level of the player
	public bool isPlayerDead; // is the player dead
	public bool isEnemyDead; // did the player kill an enemy

	public int inSection; // which section is the player in
	public int intensity;
	
	public GameObject[] players;
	//public int howManyEnemies; // how many enemies are in the section
	//public int interactableAssets; // how many interactable assets in section
	//public int backGround; // how many background assets are in the section
}
