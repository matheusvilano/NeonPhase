using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    public Transform target;
    public bool isMoving = false;
    public float particleLife;
    public ParticleSystem ps;

    private void Start()
    {
        ps.Play();
        Destroy(gameObject, particleLife);
    }

    private void Update()
    {
        if (isMoving)
        {
            gameObject.transform.position = target.transform.position;
        }
    }
}
