using UnityEngine;
using UnityEngine.SceneManagement;

public class creditsMove : MonoBehaviour {

    public float movespeed;
    public RectTransform RKT;
	
	void Update()
    {
        this.transform.Translate(Vector2.up * movespeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Joystick1Button0))
        {
            SceneManager.LoadScene("Main Menu");
        }
	}
}
