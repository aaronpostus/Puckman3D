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
        private Vector3 randomPoint;
        private Ghost ghost;
        private Vector3 destination;
        private bool computeDestination = true;
        private float range = 100f;
        private Vector3[] pathCorners;
        private float positionThreshold = 0.01f;
        private Vector3 pacmanPosition = Vector3.zero; //temporary until pacman's location is public/a property
        private float fleeTime = 6; //equal to pacman's eating state duration

        public IGhostState DoState(Ghost ghost, NavMeshAgent agent, Vector3 destination)
        {
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
            //only set destination once
            if (computeDestination)
            {
                //change to flee animation
                ChangeAnimation();

                //use navMesh to move as far from pacman's position as possible
                float farthestDistance = 0f;
                for (int i = 0; i < 10; i++)
                {
                    randomPoint = ghost.transform.position + Random.insideUnitSphere * range;
                    NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, range, NavMesh.AllAreas);
                    float distance = Vector3.Distance(hit.position, pacmanPosition); //edit
                    if (distance > farthestDistance)
                    {
                        farthestDistance = distance;
                        destination = hit.position;
                    }
                }
                agent.SetDestination(destination);
                computeDestination = false;

                //choose another destination once it reaches current destination ???
            }
            NavMeshPath path = agent.path;
            pathCorners = path.corners;

            for (int i = 0; i < pathCorners.Length; i++)
            {
                if (Mathf.Abs(ghost.transform.position.x - pathCorners[i].x) < positionThreshold && Mathf.Abs(ghost.transform.position.z - pathCorners[i].z) < positionThreshold)
                {
                    if (i+1 < pathCorners.Length)
                    {
                        Debug.DrawLine(pathCorners[i], pathCorners[i+1], Color.green);

                        direction = (pathCorners[i+1] - pathCorners[i]).normalized;

                        if (direction.z >= 0.8f && (direction.x < direction.z))
                        {
                            Debug.Log("rotate up");
                            ghost.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        }
                        else if (direction.x >= 0.8f && (direction.x > direction.z))
                        {
                            Debug.Log("rotate right");
                            ghost.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                        }
                        else if (direction.x <= -0.8f && (direction.x < direction.z))
                        {
                            Debug.Log("rotate left");
                            ghost.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                        }
                        else if (direction.z <= -0.8f)
                        {
                            Debug.Log("rotate down");
                            ghost.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        }
                    }
                }
            }
        }

        private void ChangeAnimation()
        {
            ghost.transform.GetChild(0).gameObject.SetActive(false);
            ghost.transform.GetChild(1).gameObject.SetActive(true);
        }

    }
}

