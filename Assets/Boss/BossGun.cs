using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : MonoBehaviour {

    [Range(0, 100)] public int percentToShoot = 50;
    public GameObject bulletPrefab;
    public float force = 1.5f;
    public Transform currentShootArea;
	
	public void AttemptShoot(Transform target)
    {
        int thisRun = (int) Random.Range(0, 100);

        if (thisRun <= percentToShoot)
        {
            Shoot(target);
        }
    }

    private void Shoot(Transform target)
    {
        GameObject obj = Instantiate(bulletPrefab, currentShootArea.position, currentShootArea.rotation);
        obj.GetComponent<Damager>().nameToIgnore = this.name;
        obj.GetComponent<Projectile>().projectileSpeed = force;
        obj.GetComponent<Damager>().nameToIgnore = this.name;

        if (obj.GetComponent<Projectile>().isHoming)
        {
            obj.GetComponent<Projectile>().target = target;
        }
    }
}
