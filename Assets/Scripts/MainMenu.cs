using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scenes")]
    public string levelToLoad = "Gameplay";
    public string creditsLevel = "Credit Screen";
    public string menuLevel = "Main Menu";

    [Header("GameObjects")]
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject controls;

    private void Update()
    {
        if (Input.GetKeyDown("escape") && (SceneManager.GetActiveScene() != SceneManager.GetSceneByName(menuLevel)))
        {
            SceneManager.LoadScene(menuLevel);
        }
    }

    public void PlayGame()
    {
        AkSoundEngine.SetState("GameState", "Gameplay");
        SceneManager.LoadScene(levelToLoad);
    }

    public void GoToMenu()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Menu"))
        {
            mainMenu.SetActive(true);
            controls.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(menuLevel);
        }
    }

    public void GoToCredits() => SceneManager.LoadScene(creditsLevel);

    public void QuitGame() => Application.Quit();
}
