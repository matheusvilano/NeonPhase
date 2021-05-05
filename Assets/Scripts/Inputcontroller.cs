using UnityEngine;

public class Inputcontroller : MonoBehaviour
{
    private Shooter playerShooter;
    private CharacterMove playerMover;
    //private HealthControl playerHealth;
    private SpriteControl spriteControl;
    public GameObject GunPointLeft;
    public GameObject GunPointRight;
    public NeonPhase phaser;

    public bool isMovingRight;
    public Animator anmr1;
    public Animator anmr2;
    public AnimationClip dashAnimation;

    private float angle;

    private void Awake() => GetReferences();

    private void GetReferences()
    {
        playerShooter = this.GetComponent<Shooter>();
        playerMover = this.GetComponent<CharacterMove>();
        //playerHealth = this.GetComponent<HealthControl>();
        spriteControl = this.GetComponent<SpriteControl>();
        phaser = this.GetComponent<NeonPhase>();
    }

    private void Update()
    {
        HandleMoveAxis();
        HandleAimAxis();
        HandleFaceButtons();
        HandleTrigger(Input.GetAxis("Fire"));

        if (playerMover.isGrounded)
        {
            spriteControl.Ground();
        }
    }

    private void HandleTrigger(float value)
    {
        if (value > 0.1f || Input.GetKey("k"))
        {
            playerShooter.Shoot();
            anmr1.Play("GunRecoil");
            anmr2.Play("GunRecoil");
        }
    }

    private void HandleFaceButtons()
    {
        if (Input.GetAxis("PhaseJump") > 0.1f || Input.GetKeyDown("s"))
        {
            if (phaser.activeNeon != null)
            {
                phaser.AttemptPhase();
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown("i")) //RBumper
        {
            if (isMovingRight)
            {
                playerMover.DashRight();
            }

            else
            {
                playerMover.DashLeft();
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown("w")) // LBumper
        {
            playerMover.Jump();
            spriteControl.Jump();
        }
    }

    private void HandleMoveAxis()
    {
        float xAxis = Input.GetAxis("HorizontalMove");

        if (xAxis > 0.2f || Input.GetKey("d"))
        {
            playerMover.WalkRight();
            isMovingRight = true;

            if (playerMover.isGrounded)
            {
                spriteControl.Run();
            }
        }

        else if (xAxis < -0.2f || Input.GetKey("a"))
        {
            playerMover.WalkLeft();
            isMovingRight = false;

            if (playerMover.isGrounded)
            {
                spriteControl.Run();
            }
        }

        else
        {
            isMovingRight = spriteControl.spriteRenderer.flipX;

            if (playerMover.isGrounded)
            {
                spriteControl.Idle();
                playerMover.movementMultiplier = 0.0f;
            }
        }
    }

    private void HandleAimAxis()
    {
        // FOR PC & MAC BUILDS (KEYBOARD)
        if (Input.GetKeyDown("l"))
        {
            spriteControl.FlipRight();
            playerShooter.currentShootArea = playerShooter.directRight;
            GunPointLeft.SetActive(false);
            GunPointRight.SetActive(true);
            GunPointRight.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKeyDown("j"))
        {
            spriteControl.FlipLeft();
            playerShooter.currentShootArea = playerShooter.directLeft;
            GunPointLeft.SetActive(true);
            GunPointRight.SetActive(false);
            GunPointLeft.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKeyDown("o"))
        {
            spriteControl.FlipRight();
            playerShooter.currentShootArea = playerShooter.directRight;
            GunPointLeft.SetActive(false);
            GunPointRight.SetActive(true);
            GunPointRight.transform.rotation = Quaternion.Euler(0, 180, -45);
        }

        if (Input.GetKeyDown("u"))
        {
            spriteControl.FlipLeft();
            playerShooter.currentShootArea = playerShooter.directLeft;
            GunPointLeft.SetActive(true);
            GunPointRight.SetActive(false);
            GunPointLeft.transform.rotation = Quaternion.Euler(0, 0, -45);
        }

        Vector3 inputCalc = new Vector3(Input.GetAxis("HorizontalAim"), Input.GetAxis("VerticalAim"), 0.0f);

        if (inputCalc.sqrMagnitude < 0.1f)
        {
            return;
        }

        angle = Mathf.Atan2(-Input.GetAxis("HorizontalAim"), -Input.GetAxis("VerticalAim")) * Mathf.Rad2Deg;

        if (spriteControl.isRight)
        {
            if (angle >= 135 || angle <= -150) // proper angles
            {
                GunPointRight.transform.rotation = Quaternion.Euler(180, 0, angle);
            }

            else if (angle > 80)
            {
                angle = 135;
                GunPointRight.transform.rotation = Quaternion.Euler(180, 0, angle);
            }

            else if (angle < 80 && angle > 0)
            {
                spriteControl.FlipLeft();
                playerShooter.currentShootArea = playerShooter.directLeft;
                GunPointLeft.SetActive(true);
                GunPointRight.SetActive(false);
                angle = 45;
                GunPointLeft.transform.rotation = Quaternion.Euler(0, 0, -angle);
            }

            else if (angle >= -150 && angle < -80)
            {
                angle = -150;
                GunPointRight.transform.rotation = Quaternion.Euler(180, 0, angle);
            }

            else if (angle < 0 && angle > -80)
            {
                spriteControl.FlipLeft();
                playerShooter.currentShootArea = playerShooter.directLeft;
                GunPointLeft.SetActive(true);
                GunPointRight.SetActive(false);
                angle = -30;
                GunPointLeft.transform.rotation = Quaternion.Euler(0, 0, -angle);
            }
        }

        else
        {
            if (angle <= 45 && angle >= -30)
            {
                GunPointLeft.transform.rotation = Quaternion.Euler(0, 0, -angle);
            }

            else if (angle > 0 && angle <= 100)
            {
                angle = 45;
                GunPointLeft.transform.rotation = Quaternion.Euler(0, 0, -angle);
            }

            else if (angle > 100)
            {
                spriteControl.FlipRight();
                playerShooter.currentShootArea = playerShooter.directRight;
                GunPointLeft.SetActive(false);
                GunPointRight.SetActive(true);
                angle = 135;
                GunPointRight.transform.rotation = Quaternion.Euler(180, 0, angle);
            }

            else if (angle > -100)
            {
                angle = -30;
                GunPointLeft.transform.rotation = Quaternion.Euler(0, 0, -angle);
            }

            else if (angle < -100)
            {
                spriteControl.FlipRight();
                playerShooter.currentShootArea = playerShooter.directRight;
                GunPointLeft.SetActive(false);
                GunPointRight.SetActive(true);
                angle = -30;
                GunPointRight.transform.rotation = Quaternion.Euler(180, 0, angle);
            }
        }
    }
}