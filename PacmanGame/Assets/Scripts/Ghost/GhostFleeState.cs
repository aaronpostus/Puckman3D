using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace OttiPostLewis.Lab6
{

    public class GhostFleeState : IGhostState
    {

        private Vector3 direction;
        private int speed = 3; //speed should be less than pacman's
        private float timer = 0;
        private NavMeshAgent agent;
        private Ghost ghost;
        private Vector3 destination;
        private bool computeDestination = true;
        private float range = 100f; //temporary
        private Vector3 pacmanPosition = Vector3.zero; //temporary until pacman's location is public/a property
        private float fleeTime = 6; //equal to pacman's eating state duration

        public IGhostState DoState(Ghost ghost, Vector3 direction, NavMeshAgent agent, Vector3 destination)
        {
            this.direction = direction;
            this.ghost = ghost;
            this.destination = destination;
            this.agent = agent;
            agent.speed = this.speed;
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
            //change to flee animation
            ChangeAnimation();

            //only set destination once
            if (computeDestination)
            {
                //use navMesh to move as far from pacman's position as possible
                float farthestDistance = 0f;
                for (int i = 0; i < 10; i++)
                {
                    Vector3 randomPoint = ghost.transform.position + Random.insideUnitSphere * range;
                    NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, range, NavMesh.AllAreas);
                    float distance = Vector3.Distance(hit.position, pacmanPosition);
                    if (distance > farthestDistance)
                    {
                        farthestDistance = distance;
                        destination = hit.position;
                    }
                }
                Debug.Log("setting destination");
                agent.SetDestination(destination);
                computeDestination = false;

                //choose another destination once it reaches current destination ???
            }


            //do angle stuff

        }

        private void ChangeAnimation()
        {
            ghost.transform.GetChild(0).gameObject.SetActive(false);
            ghost.transform.GetChild(1).gameObject.SetActive(true);
        }

    }
}

