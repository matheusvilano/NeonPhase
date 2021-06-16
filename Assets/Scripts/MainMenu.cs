using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scenes")]
    public string levelToLoad = "Gameplay";
    public string creditsLevel = "Credit";
    public string menuLevel = "MainMenu";

    [Header("GameObjects")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject credits;


    [Header("Audio")]
    [SerializeField] private string mainMenuState = "GameState/MainMenu";

    private void Start()
    {
        AkSoundEngine.SetState(mainMenuState.Split(new[]{'/'})[0], mainMenuState.Split(new[]{'/'})[1]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMenu();
        }
    }

    public void PlayGame()
    {
        AkSoundEngine.SetState("GameState", "Gameplay");
        SceneManager.LoadScene(levelToLoad);
    }

    public void GoToMenu()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(menuLevel))
        {
            mainMenu.SetActive(true);
            controls.SetActive(false);
            credits.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(menuLevel);
        }
    }

    public void GoToCredits() => SceneManager.LoadScene(creditsLevel);

    public void QuitGame() => Application.Quit();
}
