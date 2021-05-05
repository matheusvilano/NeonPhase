using UnityEngine;

public class Damager : MonoBehaviour
{
    public int damageValue = 1;
    public bool isConsumable = false;
    public bool resetToCheckpoint;
    public string nameToIgnore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (this.GetComponent<Projectile>())
        {
            // bullet hit
            AkSoundEngine.PostEvent("Play_Laser_Hit", this.gameObject);

            if (this.GetComponent<Projectile>().isHoming)
            {
                // missile hit
                AkSoundEngine.PostEvent("Play_Missile_Hit", this.gameObject);
            }
        }

        if (other.gameObject.tag == "Damageable" && other.name != nameToIgnore)
        {
            other.gameObject.GetComponent<HealthControl>()?.TakeDamage(damageValue);

            if (isConsumable)
            {
                Destroy(this.gameObject);
            }

            if (resetToCheckpoint && other.GetComponent<HealthControl>().canRespawn)
            {
                other.transform.parent.transform.position = other.GetComponent<HealthControl>().respawnArea.position;
            }

        }

        else if (other.gameObject.tag == "Ground")
        {
            if (isConsumable)
            {
                Destroy(this. gameObject);
            }
        }
    }
}
