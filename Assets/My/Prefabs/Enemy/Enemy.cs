using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Material AggressiveEnemy;
    public SkinnedMeshRenderer skinnedMesh;
    public Animator animatorModel;
    [HideInInspector]
    public bool isHunting = false;
    [HideInInspector]
    public Vector3 startPatrolPos;
    [HideInInspector]
    public Vector3 endPatrolPos;

    private NavMeshAgent agent;
    private bool isGoToEndPatrolPos = true;
    private Vector3 patrolTargetPos;
    private Transform currentPos;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentPos = GetComponent<Transform>();
    }
    private void Start()
    {
        agent.autoBraking = false;
        patrolTargetPos = endPatrolPos;
        agent.destination = patrolTargetPos;
        agent.speed = GameSettings.playerSpeed;
        animatorModel.SetInteger("State", 1);
    }

    private void Update()
    {
        if (Global.isPause == false)
        {
            // TODO update every n mills
            if (isHunting == false)
            {
                if (Vector3.Distance(patrolTargetPos, currentPos.position) < 0.5f)
                {
                    patrolTargetPos = ChangePatrolTarget();
                    agent.destination = patrolTargetPos;
                }
            }
        }
        else
        {
            agent.isStopped = true;
            animatorModel.SetInteger("State", 0);
        }
        
    }
    public void UpdatePatrolPositions(Vector3 startPatrolPos, Vector3 endPatrolPos)
    {
        this.startPatrolPos = startPatrolPos;
        this.endPatrolPos = endPatrolPos;
    }
    public void GoToPlayer(Vector3 playerPos)
    {
        if(isHunting == false)
        {
            isHunting = true;
            skinnedMesh.material = AggressiveEnemy;
        }
        agent.destination = playerPos;
    }
    private Vector3 ChangePatrolTarget()
    {
        if (isGoToEndPatrolPos == true)
        {
            isGoToEndPatrolPos = false;
            return startPatrolPos;
        }
        else
        {
            isGoToEndPatrolPos = true;
            return endPatrolPos;
        }
    }
}
