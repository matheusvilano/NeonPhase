using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour 
{
	public GameObject pauseUI;

    private Scene currentScene;

	private bool paused = false;

	private void Awake() => currentScene = SceneManager.GetActiveScene();

	void Start() => pauseUI.SetActive(false);

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.JoystickButton7))
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
		//Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(currentScene.buildIndex);
	}

	public void MainMenu()
	{
		//Application.LoadLevel(0);
        SceneManager.LoadScene(1);
	}

	public void Quit() => Application.Quit();
}
