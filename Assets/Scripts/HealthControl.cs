using UnityEngine;

public class HealthControl : MonoBehaviour
{
    public int maxHealth;
	public int currentHealth;

    public bool canRespawn;
    public Transform respawnArea;
    public Animator anmr;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anmr.Play("Damage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (canRespawn)
        {
            AkSoundEngine.SetState("GameState", "Gameplay");
            LevelControl.instance.ResetLevel();
        }

        else
        {
            Debug.Log(gameObject.name+" will be destroyed via HealthControl");
            Destroy(gameObject);
        }
    }

    public void Heal(int heal)
    {
        currentHealth += heal;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
