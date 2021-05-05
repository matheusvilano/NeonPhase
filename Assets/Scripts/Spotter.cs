using UnityEngine;

public class Spotter : MonoBehaviour
{
    public HumanAI enemyAI;
    public HorseAI horseAI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "Player")
        {
            if (horseAI)
            {
                horseAI.playersInRange.Add(other.gameObject);
            }

            if (enemyAI)
            {
                enemyAI.playersInRange.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "Player")
        {
            if (horseAI)
            {
                horseAI.playersInRange.Remove(other.gameObject);
            }

            if (enemyAI)
            {
                enemyAI.playersInRange.Remove(other.gameObject);
            }
        }
    }
}
