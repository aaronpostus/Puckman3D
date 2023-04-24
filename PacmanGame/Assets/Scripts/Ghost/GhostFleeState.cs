using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace OttiPostLewis.Lab6
{

    public class GhostFleeState : IGhostState
    {
        private int speed = 3; //speed should be less than pacman's
        private float timer = 0;
        private NavMeshAgent agent;
        private Vector3 randomPoint;
        private Ghost ghost;
        private Vector3 destination;
        private bool computeDestination = true;
        private float range = 100f;
        private float positionThreshold = 0.01f;
        private float newDistance;
        private float currentDistance;
        private Vector3 pacmanPosition = Vector3.zero; //temporary until pacman's location is public/a property
        private float fleeTime = 6; //equal to pacman's eating state duration

        public IGhostState DoState(Ghost ghost, NavMeshAgent agent, Vector3 destination)
        {
            this.ghost = ghost;
            this.destination = destination;
            this.agent = agent;
            agent.speed = this.speed * Ghost.multiplier;
            agent.updateRotation = false;
            Flee();

            timer += Time.deltaTime;
            if (timer > fleeTime)
            {
                timer = 0;
                computeDestination = true;
                return ghost.wanderState;
            }
            return ghost.fleeState;
        }

        private void Flee()
        {
            //only set destination once
            if (computeDestination)
            {
                //change to flee animation
                ChangeAnimation();

                //use navMesh to move as far from pacman's position as possible
                currentDistance = 0f;
                for (int i = 0; i < 10; i++)
                {
                    randomPoint = ghost.transform.position + Random.insideUnitSphere * range;
                    NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, range, NavMesh.AllAreas);
                    newDistance = Vector3.Distance(hit.position, pacmanPosition);
                    if (newDistance > currentDistance)
                    {
                        currentDistance = newDistance;
                        destination = hit.position;
                    }
                }
                agent.SetDestination(destination);
                computeDestination = false;

                //choose another destination once it reaches current destination ???
            }
            NavMeshPath path = agent.path;
            GhostUtility.RotateAtCorners(ghost, path, positionThreshold);
        }

        private void ChangeAnimation()
        {
            ghost.transform.GetChild(0).gameObject.SetActive(false);
            ghost.transform.GetChild(1).gameObject.SetActive(true);
        }

    }
}

