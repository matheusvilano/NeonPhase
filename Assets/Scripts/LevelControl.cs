using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public static LevelControl instance;
    public int intensity = 0;
    public Camera testCamera;
    public ParticleSystem weatherSystem;
    private Scene currentScene;

    private void Awake() => currentScene = SceneManager.GetActiveScene();

    private void Start() => instance = instance ?? this;

    private void Update()
    {
        if (Input.GetKeyDown("="))
        {
            ResetLevel();
        }
    }

    public void ResetLevel()
    {
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void WinGame()
    {

    }
    
    public void ChangeIntensity(int newIntensity)
    {
        ParticleSystem.MainModule main = weatherSystem.main;

        intensity = newIntensity;

        if (intensity >= 10)
        {
            main.simulationSpeed = 20;
        }

        else
        {
            switch (intensity)
            {
                case 0: main.simulationSpeed = 10.0f; break;
                case 1: main.simulationSpeed = 11.0f; break;
                case 2: main.simulationSpeed = 12.0f; break;
                case 3: main.simulationSpeed = 13.0f; break;
                case 4: main.simulationSpeed = 14.0f; break;
                case 5: main.simulationSpeed = 15.0f; break;
                case 6: main.simulationSpeed = 16.0f; break;
                case 7: main.simulationSpeed = 17.0f; break;
                case 8: main.simulationSpeed = 18.0f; break;
                case 9: main.simulationSpeed = 19.0f; break;
                default: Debug.Log($"{this.ToString()}: Invalid new intensity."); break;
            }
        }
    }
}
