using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public Sprite[] Badge; 
	public Image healthUI;
	public HealthControl playerHealth;

	void Update() => healthUI.sprite = Badge[playerHealth.currentHealth]; 
}
