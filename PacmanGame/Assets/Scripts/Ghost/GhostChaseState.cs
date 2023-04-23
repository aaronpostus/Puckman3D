using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace OttiPostLewis.Lab6
{
    public class GhostChaseState : IGhostState
    {
        private NavMeshAgent agent;
        private Ghost ghost;
        private Vector3 destination;
        private float positionThreshold = 0.01f;

        public IGhostState DoState(Ghost ghost, NavMeshAgent agent, Vector3 destination)
        {
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
            agent.SetDestination(new Vector3(destination.x, ghost.transform.position.y, destination.z)); 

            //do angle stuff? prob not neccessary bc will just be going in a straight line

        }

    }
}