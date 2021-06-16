using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour 
{
	public static bool paused = false;
	public GameObject pauseUI;

    private Scene currentScene;

	private void Awake() => currentScene = SceneManager.GetActiveScene();

	void Start() => pauseUI.SetActive(false);

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.Escape))
		{
			paused = !paused;
		}

		if(paused)
		{
			AkSoundEngine.SetState("IsPaused", "True");
			pauseUI.SetActive(true);
			Time.timeScale = 0;
		}

		if(!paused)
		{
			AkSoundEngine.SetState("IsPaused", "False");
			pauseUI.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void Restart()
	{
        SceneManager.LoadScene("GameplayFinal");
	}

	public void MainMenu()
	{
		// Update game state
		AkSoundEngine.SetState("GameState", "MainMenu");

		// Stop any dialogue by playing silence
		CheckPoint.StopAllDialogue(this.gameObject);

		// Load main menu
        SceneManager.LoadScene("MainMenu");
	}

	private void OnDestroy()
{
		AkSoundEngine.SetState("IsPaused", "False");
		pauseUI.SetActive(false);
		Time.timeScale = 1;
		enabled = false;
		paused = false;
	}

	public void Quit()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
		#else
			UnityEngine.Application.Quit();
		#endif
	}
}