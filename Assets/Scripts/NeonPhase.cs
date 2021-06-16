using UnityEngine;

public class NeonPhase : MonoBehaviour
{
    // Predefined Wwise Events (they will NOT change)
    private const string playPhaseWwiseEvent = "Play_Player_Phase";
    private const string stopPhaseWwiseEvent = "Stop_Player_Phase";

    public Transform origin;
    public Color neonColor;
    public Transform dest;

    public bool isPhasing = false;
    public static bool recentlyPhased = false;
    public float speed;

    public SpriteRenderer[] spritesToDisable;
    public SpriteRenderer[] spritestoEnable;

    public Neon activeNeon;
    float currentCooldown;
    public float Cooldown = 1.0f;
    bool canPhase = true;

    void Update()
    {
        if (!canPhase)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (currentCooldown <= 0)
        {
            canPhase = true;
        }

        if (isPhasing)
        {
            Phase(origin, dest);
        }
    }

    public void AttemptPhase()
    {
        if (canPhase)
        {
            StartPhase();
            canPhase = false;
            Debug.Log("PhaseStart");
        }
    }

    public void StartPhase()
    {
        // Sound
        AkSoundEngine.PostEvent(NeonPhase.playPhaseWwiseEvent, this.gameObject);

        // Physics
        Debug.Log("Origin X: "+origin.position.x+ "Dest X: " + dest.position.x);
        this.transform.position = origin.position;
        canPhase = false;
        currentCooldown = Cooldown;
        GetComponent<InputController>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; 
        GetComponent<SpriteControl>().DoPhase(neonColor);
        isPhasing = true;

        // Sprite
        foreach (SpriteRenderer item in spritesToDisable)
        {
            item.enabled = false;
        }

        foreach (SpriteRenderer item in spritestoEnable)
        {
            item.enabled = true;
        }

        if (Mathf.Abs(origin.position.x - dest.position.x) < 0.2f)
        {
            if (origin.position.y < dest.position.y)
            {
                foreach (SpriteRenderer item in spritestoEnable)
                {
                    item.flipX = true;
                }
            }

            else
            {
                foreach (SpriteRenderer item in spritestoEnable)
                {
                    item.flipX = false;
                }
            }
        }

        else if (origin.position.x > dest.position.x)
        {
            foreach (SpriteRenderer item in spritestoEnable)
            {
                item.flipX = false;
            }
        }

        else
        {
            foreach (SpriteRenderer item in spritestoEnable)
            {
                item.flipX = true;
            }
        }
    }

    public void Phase(Transform Origin, Transform Destination)
    {
        var value = Vector3.MoveTowards(transform.position, Destination.position, speed);
        transform.position = value;

        if (Vector2.Distance(transform.position, Destination.position) < 0.2f)
        {
            EndPhase();
        }
    }

    public void EndPhase()
    {  
        // Sound: stop phasing loop
        AkSoundEngine.PostEvent(NeonPhase.stopPhaseWwiseEvent, this.gameObject);

        // Bool
        recentlyPhased = true;      
        print("recentlyPhased = true");
        Invoke("SoundDebug", 0.3f);

        // Physics
        isPhasing = false;
        GetComponent<InputController>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<SpriteControl>().EndPhase();
        // activeNeon.AtEnd(this.gameObject);

        // Sprite
        foreach (SpriteRenderer item in spritesToDisable)
        {
            item.enabled = true;
        }

        foreach (SpriteRenderer item in spritestoEnable)
        {
            item.enabled = false;
        }
    }

    public void SoundDebug()
    {
        recentlyPhased = false;
        print("recentlyPhased = false");
        CancelInvoke("SoundDebug");
    }
}
