using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPCFSMState : MonoBehaviour
{

    public GameObject[] wanderPoints;
    public float NPCSpeed = 5;
    public float chaseDistance = 10;

    Animator anim;
    Vector3 nextDestination;
    int currentDesinationindex = 0;


    public GameObject player;
    public float fieldOFView = 45f;
    float elapseTime = 0;

    Transform deadTransform;

    NavMeshAgent agent;
    private NpcInteraction interact;
    public Transform enemyEyes;

    public enum FSMStates
    {
        Patrol,
        Talk
    }

    public FSMStates currentState;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wanderPoints = GameObject.FindGameObjectsWithTag("Wanderpoint");
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        Initialize();
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Talk:
                UpdateTalkState();
                break;
        }
        elapseTime += Time.deltaTime;
    }

    void Initialize()
    {
        currentState = FSMStates.Patrol;
        FindNextPoint();
    }

    void UpdatePatrolState()
    {
        print("Patroling!");
        anim.SetInteger("animState", 2);

        agent.stoppingDistance = 0;

        agent.speed = 2f;

        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();

        } else if (Input.GetKeyDown(KeyCode.E) || IsPlayerInClearFOV())
        {
            anim.SetInteger("animState", 0);
            FaceTarget(player.transform.position);
            currentState = FSMStates.Talk;
            agent.speed = 0;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);

    }

    

    void UpdateTalkState()
    {
        anim.SetInteger("animState", 1);
        agent.speed = 0;
        FaceTarget(player.transform.position);
        if (!IsPlayerInClearFOV())
        {
            currentState = FSMStates.Patrol;
        }
    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDesinationindex].transform.position;

        currentDesinationindex = (currentDesinationindex + 1) % wanderPoints.Length;

        agent.SetDestination(nextDestination);
    }

    public void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }



    private void OnDrawGizmos()
    {

        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOFView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOFView * 0.5f, 0) * frontRayPoint;

        Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
        Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.yellow);
        Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.magenta);
    }

    bool IsPlayerInClearFOV()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;

        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOFView)
        {
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    print("Player in sight!");
                    agent.speed = 0f;
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }
}

