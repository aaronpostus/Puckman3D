using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostChaseState : IGhostState
{

    private Vector3 direction;
    private int speed = 5;
    private NavMeshAgent agent;
    private Ghost ghost;
    public IGhostState DoState(Ghost ghost, Vector3 direction, NavMeshAgent agent, Vector3 destination)
    {
        this.direction = direction;
        this.ghost = ghost;
        this.agent = agent;
        Chase();
        //return chase until ghost location = last known location of pacman
        //if (ghost.transform.position.x == destination.x && ghost.transform.position.y == destination.y)
        //{
        //    return ghost.wanderState;
        //}
        return ghost.chaseState;
    }

    private void Chase()
    {
        Debug.Log("ghost chasing");
        agent.SetDestination(new Vector3(-3.5f, ghost.transform.position.y, -1.97f));
        Debug.Log(agent.isOnNavMesh);
        //go to last location pacman was at then go back to wander state; may need location parameter in DoState... 
        //ghost.transform.rotation = Quaternion.LookRotation(direction);
        //ghost.transform.localPosition += ghost.transform.forward * Time.deltaTime * speed;
    }

}