using System.Collections.Generic;
using UnityEngine;

public class HorseAI : MonoBehaviour
{
    private bool jump;
    private bool isRunning;
    private bool isIdle;

    public Transform target;

    public Transform patrolA;
    public Transform patrolB;

    public bool isPatrollingAToB = true;

    public HealthControl enemyHealth;
    public CharacterMove mover;
    public Shooter shooterControl;
    public LevelClass levelManager;
    public SpriteControl spritecontroller;

    public GameObject GunPointLeft;
    public GameObject GunPointRight;
    public GameObject spotter;

    public List<GameObject> playersInRange;

    public enum AIstate { Wander, Aiming };
    public AIstate enemyState;

    private void Start() => GetReferences();

    private void GetReferences()
    {
        enemyHealth = GetComponent<HealthControl>();
        mover = GetComponent<CharacterMove>();
        shooterControl = GetComponent<Shooter>();
    }

    private void UpdateTarget(GameObject[] targetsToCompare)
    {
        if (targetsToCompare != null)
        {
            target = targetsToCompare[0].transform;

            foreach (GameObject item in targetsToCompare)
            {
                float currentDistance = Vector2.Distance(this.transform.position, target.position);
                float newDistance = Vector2.Distance(this.transform.position, item.transform.position);

                if (newDistance < currentDistance)
                {
                    target = item.transform;
                }
            }
        }
    }

    private void Update()
    {
        if (playersInRange.Count > 0)
        {
            UpdateTarget(playersInRange.ToArray());
            AimAtTarget();
            shooterControl.targetForHomingBullets = target;
            shooterControl.Shoot();
        }

        else if (isPatrollingAToB)
        {
            spritecontroller.FlipRight();
        }

        else if (!isPatrollingAToB)
        {
            spritecontroller.FlipLeft();
        }

        if (spritecontroller.isRight)
        {
            GunPointRight.SetActive(true);
            GunPointLeft.SetActive(false);
            spotter.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        else if (!spritecontroller.isRight)
        {
            GunPointRight.SetActive(false);
            GunPointLeft.SetActive(true);
            spotter.transform.rotation = new Quaternion(0, 180, 0, 0);
        }

        Wander();
    }

    private void AimAtTarget()
    {
        // look to the Left
        if (target.position.x <= transform.position.x)
        {
            spritecontroller.FlipLeft();
            shooterControl.currentShootArea = shooterControl.directLeft;
        }

        //look to the Right
        else if (target.position.x > transform.position.x)
        {
            spritecontroller.FlipRight();
            shooterControl.currentShootArea = shooterControl.directRight;
        }

        if (spritecontroller.isRight)
        {
            GunPointRight.transform.LookAt(target);
        }

        else
        {
            GunPointLeft.transform.LookAt(target);
        }

    }

    private void Wander()
    {
        spritecontroller.Run();

        if (isPatrollingAToB)
        {
            shooterControl.currentShootArea = shooterControl.directRight;

            mover.WalkRight();

            if (Vector2.Distance(this.transform.position, patrolB.position) < 0.3f)
            {
                isPatrollingAToB = false;
            }
        }

        else
        {
            mover.WalkLeft();

            shooterControl.currentShootArea = shooterControl.directLeft;

            if (Vector2.Distance(this.transform.position, patrolA.position) < 1f)
            {
                isPatrollingAToB = true;
            }
        }
    }
}
