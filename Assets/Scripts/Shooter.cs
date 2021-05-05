using UnityEngine;

public class Shooter : MonoBehaviour
{
    public bool isBlast; // is the shot a blast
    public bool isShot; // is the shot a bullet

    //public bool playerShoot; // is the player shooting with RT
    //public bool enemyShoot; // is the enemy shooting

    public float coolDown = 0.5f; // is the cool down at 0
    public bool canShoot = true; // can the player shoot
    public float currentCoolDown; // the current cooldown of the weapon 
    public GameObject bulletPrefab;

    public Transform currentShootArea;

    public Transform directRight;
    public Transform directLeft;
    public float force;
    public Transform targetForHomingBullets;

    public Animator anmr;

    public void Awake() => anmr = this.GetComponent<Animator>();

    public void Shoot()
    {
        if (canShoot)
        {
            Debug.Log(gameObject.name);
            GameObject projectile = Instantiate(bulletPrefab, currentShootArea.position, currentShootArea.rotation);
            projectile.GetComponent<Projectile>().projectileSpeed = force;
            
            if (this.gameObject.name == "Player")
            {
                AkSoundEngine.PostEvent("Play_Player_Gunshot", projectile);
            }

            else if (this.gameObject.name == "Human")
            {
                AkSoundEngine.PostEvent("Play_Enemy_Gunshot", projectile);
            }

            else if (this.gameObject.name == "Horse")
            {
                AkSoundEngine.PostEvent("Play_Missile_Launch", projectile);
            }

            if (projectile.GetComponent<Projectile>().isHoming)
            {
                projectile.GetComponent<Projectile>().target = targetForHomingBullets;
            }

            currentCoolDown = coolDown;
            canShoot = false;
        }
    }

    private void Update()
    {
        if (!canShoot)
        {
            currentCoolDown -= Time.deltaTime;
        }

        if (currentCoolDown <= 0.0f)
        {
            canShoot = true;
        }
    }
}
