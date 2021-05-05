using UnityEngine;

public partial class CharacterMove : MonoBehaviour
{
    public float movementSpeed; // player's movement speed
    public int jumpForce;
    public int dashMovement; // when the player dashes
    public bool isCrouching; // is the player crouching
    public bool canDash = true; // is the player able to dash
    public bool isGrounded = true; // is the player on the ground
    public float dashCoolDown; // cooldown time of player dash
    public float currentDashCoolDown; // current cooldown time of the dash
    private int _currentSurface; // what the player is stepping on (8 = Concrete | 9 = Metal)
    private bool gameStartedRecently = true;
    public float movementMultiplier = 0.0f; // change to zero in order to have a smooth transition
    public bool isReadyToMove = false;

    public void WalkRight()
    {
        gameObject.transform.Translate(Vector2.right * movementMultiplier * movementSpeed * Time.deltaTime);
        movementMultiplier += (movementMultiplier >= 1.0f) ? 0.0f : (Time.deltaTime * 10);
    }

    public void WalkLeft()
    {
        gameObject.transform.Translate(Vector2.left * movementMultiplier * movementSpeed * Time.deltaTime);
        movementMultiplier += (movementMultiplier >= 1.0f) ? 0.0f : (Time.deltaTime * 10);
    }

    public void OnFootstep()
    {
        if (this.gameObject.name == "Player")
        {
            AkSoundEngine.PostEvent("Play_Player_Footstep", this.gameObject);
        }
        else if (this.gameObject.name == "Human")
        {
            AkSoundEngine.PostEvent("Play_Enemy_Footstep", this.gameObject);
        }
    }
    
    private void UpdateCooldown()
    {
        currentDashCoolDown -= !canDash ? Time.deltaTime : 0.0f;
        canDash = currentDashCoolDown <= 0.0f;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * jumpForce);
            isGrounded = false;
            AkSoundEngine.PostEvent("Play_Player_Jump", this.gameObject);
        }
    }

    private void EvaluateCollision(ref Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            _currentSurface = other.gameObject.layer;

            switch (_currentSurface)
            {
                case 8: AkSoundEngine.SetSwitch("Surface", "Concrete", this.gameObject); break;
                case 9: AkSoundEngine.SetSwitch("Surface", "Metal", this.gameObject); break;
                default: Debug.Log($"{this.ToString()}: Invalid surface for an object tagged 'Ground'"); break;
            }

            if (!NeonPhase.recentlyPhased && !gameStartedRecently)
            {
                AkSoundEngine.PostEvent("Play_Player_Land", this.gameObject);
            }
            else if (gameStartedRecently)
            {
                gameStartedRecently = !gameStartedRecently;
            }
        }
    }

    public void DashRight()
    {
        if (canDash)
        {
            gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * dashMovement);
            currentDashCoolDown = dashCoolDown;
            canDash = false;
            this.GetComponent<Animator>().Play("Dash");
            AkSoundEngine.PostEvent("Play_Player_Dash", this.gameObject);
        }
    }

    public void DashLeft()
    {
        if (canDash)
        {
            gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * dashMovement);
            currentDashCoolDown = dashCoolDown;
            canDash = false;
            this.GetComponent<Animator>().Play("Dash");
            AkSoundEngine.PostEvent("Play_Player_Dash", this.gameObject);
        }
    }
}
