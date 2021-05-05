using System.Collections.Generic;
using UnityEngine;

public class HumanAI : MonoBehaviour
{
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

    private void Start() => SetReferences();

    private void Update()
    {
        UpdateUnitMovement();
        UpdateGunSpriteOrientation();
        UpdateUnitState();
        EvaluateUnitState();
    }

    private void SetReferences()
    {
        enemyHealth = GetComponent<HealthControl>();
        mover = GetComponent<CharacterMove>();
        shooterControl = GetComponent<Shooter>();
    }

    private void UpdateTarget(GameObject[] targetsToCompare)
    {
        if (targetsToCompare != null)
        {
            enemyState = AIstate.Aiming;
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

        else
        {
            enemyState = AIstate.Wander;
        }
    }

#pragma warning disable
    private void UpdateUnitMovement()
    {
        if (mover.movementSpeed == 0 || enemyState == AIstate.Aiming) // mover.movementSpeed == 0
        {
            this.GetComponent<Animator>().SetBool("isRunning", false);
            this.GetComponent<Animator>().SetBool("isIdle", true);
        }
        else
        {
            spritecontroller.Run();
        }
    }
#pragma warning enable

    private void UpdateGunSpriteOrientation()
    {
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
    }

    private void UpdateUnitState()
    {
        if (playersInRange.Count > 0)
        {
            UpdateTarget(playersInRange.ToArray());
            enemyState = AIstate.Aiming;
        }
        else
        {
            enemyState = AIstate.Wander;
        }
    }

    private void EvaluateUnitState()
    {
        if (enemyState == AIstate.Aiming)
        {
            AimAtTarget();
            shooterControl.Shoot();
        }
        else if (enemyState == AIstate.Wander)
        {
            Wander();
        }
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

        shooterControl.Shoot();
    }

    private void Wander()
    {
        if (isPatrollingAToB)
        {
            spritecontroller.FlipRight();
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
            spritecontroller.FlipLeft();
            shooterControl.currentShootArea = shooterControl.directLeft;

            if (Vector2.Distance(this.transform.position, patrolA.position) < 1.0f)
            {
                isPatrollingAToB = true;
            }
        }
    }
}
