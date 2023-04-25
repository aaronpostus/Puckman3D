using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Author: Maddi
namespace OttiPostLewis.Lab6
{

    public class GhostFleeState : IGhostState
    {
        private float speed = 3.5f; //speed should be less than pacman's
        private NavMeshAgent agent;
        private Vector3 randomPoint;
        private Ghost ghost;
        private Vector3 destination;
        private bool computeDestination = true;
        private float range = 100f;
        private float positionThreshold = 0.01f;
        private float newDistance;
        private float currentDistance;

        public IGhostState DoState(Ghost ghost, NavMeshAgent agent, Vector3 destination, bool computeInitialDest)
        {
            this.ghost = ghost;
            this.agent = agent;
            agent.speed = this.speed * Ghost.multiplier;
            agent.updateRotation = false;
            Flee();

            //when pacman changes back to normal state, revert animation and begin wandering
            if (MovementControl.currentState == MovementControl.PacmanState.Flee)
            {
                Debug.Log("pacman is normal now");
                computeDestination = true;
                ghost.computeInitialDest = true;
                RevertAnimation();
                return ghost.wanderState;
            }
            return ghost.fleeState;
        }

        private void Flee()
        {
            //only set destination once
            if ((Mathf.Abs(ghost.transform.position.x - destination.x) < positionThreshold && Mathf.Abs(ghost.transform.position.z - destination.z) < positionThreshold) || computeDestination)
            {
                //change to flee animation
                ChangeAnimation();

                //use navMesh to move as far from pacman's position as possible
                currentDistance = 0f;
                for (int i = 0; i < 10; i++)
                {
                    randomPoint = ghost.transform.position + Random.insideUnitSphere * range;
                    NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, range, NavMesh.AllAreas);
                    newDistance = Vector3.Distance(hit.position, MovementControl.playerTransform.position);
                    if (newDistance > currentDistance)
                    {
                        currentDistance = newDistance;
                        destination = hit.position;
                    }
                }
                agent.SetDestination(destination);
                computeDestination = false;

                
            }
            NavMeshPath path = agent.path;
            GhostUtility.RotateAtCorners(ghost, path, positionThreshold);
        }

        private void ChangeAnimation()
        {
            ghost.transform.GetChild(0).gameObject.SetActive(false);
            ghost.transform.GetChild(1).gameObject.SetActive(true);
        }

        private void RevertAnimation()
        {
            ghost.transform.GetChild(0).gameObject.SetActive(true);
            ghost.transform.GetChild(1).gameObject.SetActive(false);
        }

    }
}

