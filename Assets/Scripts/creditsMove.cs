using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMove : MonoBehaviour {

    [SerializeField] private int maxPositionY = 1200;
    [SerializeField] private float movespeed;
    [SerializeField] private RectTransform RKT;
	
	private void Update()
    {
        this.transform.Translate(Vector2.up * movespeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Escape) || transform.position.y > maxPositionY)
        {
            SceneManager.LoadScene("MainMenu");
        }
	}
}
