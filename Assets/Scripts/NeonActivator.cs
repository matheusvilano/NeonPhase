using UnityEngine;

public class NeonActivator : MonoBehaviour
{
    public Transform origin;
    public Transform dest;
    public Neon fatherNeon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && fatherNeon.isActiveAndEnabled)
        {
            var phaser = other.GetComponent<NeonPhase>();

            if (!phaser.isPhasing)
            {
                fatherNeon.locationA = origin;
                fatherNeon.locationB = dest;
                fatherNeon.GetComponent<Renderer>().material.color = Color.red;
                phaser.activeNeon = fatherNeon;
                phaser.origin = origin;
                phaser.dest = dest;
                phaser.neonColor = fatherNeon.neonColor;

                foreach (SpriteRenderer item in phaser.spritestoEnable)
                {
                    item.color = fatherNeon.neonColor;
                    item.gameObject.transform.rotation = fatherNeon.transform.rotation;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var phaser = other.GetComponent<NeonPhase>();

            if (!phaser.isPhasing)
            {
                fatherNeon.GetComponent<Renderer>().material.color = Color.white;
                phaser.activeNeon = null;
            }
        }
    }
}
