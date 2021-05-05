using UnityEngine;

public class Neon : MonoBehaviour
{
    public Transform locationA;
    public Transform locationB;
    public Color neonColor;
    public float refreshRate;
    public Vector2 forceDirection;

    public void Disable()
    {
        this.gameObject.SetActive(false);
        Invoke("Activate", refreshRate); // TODO CHANGE
    }

    public void AtEnd(GameObject phaser)
    {
        phaser.GetComponent<NeonPhase>().activeNeon = null;
        phaser.GetComponent<Rigidbody2D>().AddForce(forceDirection);
    }

    void Activate() => this.gameObject.SetActive(true);
}
