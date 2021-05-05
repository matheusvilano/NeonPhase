using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAI : MonoBehaviour
{
    public BossGun[] guns;
    public float firstFireRate = 0.6f;
    public float secondFireRate = 0.4f;
    private float currentCooldown;
    private float cooldown;

    public HealthControl shoulderHealth;
    public HealthControl mouthHealth;
    public Transform target;
    public Animator anmr;

    public enum AIState { Idle, OnCombat, LostShoulder, Dead };
    public AIState thisAI;

    private void Start() => guns = GetComponentsInChildren<BossGun>();

    private void LoadCredits()
    {
        AkSoundEngine.SetState("GameState", "MainMenu");
        SceneManager.LoadScene("Credit Screen");
    }

    private void Attack()
    {
        currentCooldown -= Time.deltaTime;

        if (currentCooldown <= 0)
        {
            foreach (BossGun item in guns)
            {
                item.AttemptShoot(target);
            }
            currentCooldown = cooldown;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && thisAI == AIState.Idle)
        {
            guns = GetComponentsInChildren<BossGun>();
            thisAI = AIState.OnCombat;
            //start boss music
            AkSoundEngine.PostEvent("Play_Boss_Wings", gameObject);
        }
    }

    void Update()
    {
        if (thisAI == AIState.Idle)
        {

        }

        if (thisAI == AIState.OnCombat)
        {
            cooldown = firstFireRate;

            if (!shoulderHealth) // Shoulder is Dead 
            {
                Debug.Log("The AI went LostShoulder.");
                thisAI = AIState.LostShoulder;

                guns = GetComponentsInChildren<BossGun>();
                mouthHealth.gameObject.SetActive(true);
            }

            Attack();
        }

        if (thisAI == AIState.LostShoulder)
        {
            anmr.SetBool("LostShoulder", true);
            anmr.SetBool("Healthy", false);
            anmr.SetBool("Dead", true);
            guns = GetComponentsInChildren<BossGun>();
            cooldown = secondFireRate;

            if (!mouthHealth)
            {
                thisAI = AIState.Dead;
                Debug.Log("the AI went Dead.");
                //Sound for play Death
                AkSoundEngine.PostEvent("Play_Boss_Explosion", this.gameObject);
                AkSoundEngine.SetState("BossState", "Dead");
                GameObject.Find("AudioPlot5")?.GetComponent<CheckPoint>()?.PlayDialogue(AkSoundEngine.GetIDFromString("BossDefeated")); //AkSoundEngine.PostEvent("Play_VO_BossDeath", this.gameObject);
                Invoke("LoadCredits", 4.0f);
            }

            Attack();
        }

        if (thisAI == AIState.Dead)
        {
            anmr.GetComponent<Animator>().SetBool("LostShoulder", false);
            anmr.GetComponent<Animator>().SetBool("Healthy", false);
            anmr.GetComponent<Animator>().SetBool("Dead", true);
            anmr.Play("Death");
            // LevelControl.instance.WinGame();
            Destroy(gameObject,4);
        }
    }
}
