using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 1;
    public int damageDealt = 20;
    public float lifeTime;
    public bool isHoming;
    public Transform target;
    public bool canDestroyBullets;
    public bool destructable;

    private void StopProjectileSound() => AkSoundEngine.PostEvent("Stop_Player_Gunshot", this.gameObject);

    private void MoveBullet()
    {
        if (isHoming)
        {
            this.transform.LookAt(target);
            this.transform.position = Vector2.MoveTowards(transform.position, target.position, projectileSpeed);
            this.transform.Rotate(0.0f, 0.0f, this.transform.rotation.y);
        }

        else
        {
            gameObject.transform.Translate(Vector2.right * projectileSpeed);
        }
    }

    private void Start() => Invoke("DestroyProjectile", lifeTime);

    private void Update() => MoveBullet();

    private void DestroyProjectile() => Destroy(this.gameObject);

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject other = otherCollider.gameObject;

        if (other.GetComponent<Projectile>())
        {
            if (other.gameObject.GetComponent<Projectile>().canDestroyBullets == true && destructable)
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnDestroy() => StopProjectileSound();
}
