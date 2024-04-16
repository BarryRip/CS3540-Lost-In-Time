using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPCFSMChase : MonoBehaviour
{
    public GameObject[] wanderPoints;
    public float NPCSpeed = 5;
    public float chaseDistance = 10;
    public float attackDistance = 5;
    float distanceToPlayer;

    private Animator anim;
    private int currentDestinationIndex = 0;

    private GameObject player;
    public  Transform npcEyes;
    private NavMeshAgent agent;
    Vector3 nextDestination;
    public enum FSMStates
    {
        Patrol,
        Talk,
        Chase
    }

    public FSMStates currentState;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //npcEyes = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        Initialize();
    }

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
            case FSMStates.Chase:
                UpdateChaseState();
                break;
        }
    }

    void Initialize()
    {
        currentState = FSMStates.Patrol;
        FindNextPoint();
    }

    void UpdatePatrolState()
    {
        anim.SetInteger("animState", 2);
        agent.speed = 2f;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            FindNextPoint();
        }
        else if (Input.GetKeyDown(KeyCode.E) || IsPlayerInClearFOV())
        {
            anim.SetInteger("animState", 0);
            FaceTarget(player.transform.position);
            currentState = FSMStates.Talk;
            agent.speed = 0;
        }
        else
        {
            FaceTarget(nextDestination);
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

    void UpdateChaseState()
    {
        print("Chasing");

        anim.SetInteger("animState", 3);

        nextDestination = player.transform.position;

        agent.stoppingDistance = attackDistance;

        agent.speed = 5;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Talk;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);
    }

    void FindNextPoint()
    {
        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;
        agent.SetDestination(nextDestination);
    }

    public void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    bool IsPlayerInClearFOV()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.transform.position - npcEyes.position;

        if (Vector3.Angle(directionToPlayer, npcEyes.forward) <= 45f)
        {
            if (Physics.Raycast(npcEyes.position, directionToPlayer, out hit, chaseDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }
}
