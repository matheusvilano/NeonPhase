using UnityEngine;

public class SpriteControl : MonoBehaviour
{
    public bool isRight = true;
    public SpriteRenderer spriteRenderer;
    private Animator animator;
    public GameObject[] Flippers;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Ground()
    {
        // animr.SetBool("isIdle", true);
        // animr.SetBool("isRunning", false);
        animator.SetBool("Jump", false);
    }

    public void FlipLeft()
    {
        spriteRenderer.flipX = false;
        isRight = false;

        foreach (GameObject item in Flippers)
        {
            item.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    public void FlipRight()
    {
        spriteRenderer.flipX = true;
        isRight = true;
        foreach (GameObject item in Flippers)
        {
            item.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public void Stand() => Idle();

    public void Idle()
    {
        animator.SetBool("isIdle", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("Jump", false);
    }

    public void Run()
    {
        if (gameObject.name != "Horse") {
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
            animator.SetBool("Jump", false);
        }
    }

    public void Jump()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("Jump", true);
    }

    public void DoPhase(Color colorOfPhase)
    {
        spriteRenderer.color = colorOfPhase;
        animator.SetBool("IsPhase", true);
    }

    public void EndPhase()
    {
        spriteRenderer.color = Color.white; // TODO CHANGE TO ORIGINAL COLOR;
        animator.SetBool("IsPhase", false);
    }
}
