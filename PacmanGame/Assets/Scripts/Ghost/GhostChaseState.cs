using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace OttiPostLewis.Lab6
{
    public class GhostChaseState : IGhostState
    {

        private Vector3 direction;
        private int speed = 5;
        private NavMeshAgent agent;
        private Ghost ghost;
        private Vector3 destination;
        private float positionThreshold = 0.01f;

        public IGhostState DoState(Ghost ghost, Vector3 direction, NavMeshAgent agent, Vector3 destination)
        {
            this.direction = direction;
            this.ghost = ghost;
            this.agent = agent;
            this.destination = destination;
            Chase();
            //return chase until ghost location = last known location of pacman
            if (Mathf.Abs(ghost.transform.position.x - destination.x) < positionThreshold && Mathf.Abs(ghost.transform.position.z - destination.z) < positionThreshold)
            {
                return ghost.wanderState;
            }
            return ghost.chaseState;
        }

        private void Chase()
        {
            Debug.Log("ghost chasing");
            //agent.SetDestination(new Vector3(-3.5f, ghost.transform.position.y, -1.97f));
            //Debug.Log(agent.isOnNavMesh);

            agent.SetDestination(new Vector3(destination.x, ghost.transform.position.y, destination.z)); 
        }

    }
}