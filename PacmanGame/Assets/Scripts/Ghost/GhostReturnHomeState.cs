using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace OttiPostLewis.Lab6
{

    public class GhostReturnHomeState : IGhostState
    {

        private Vector3 direction;
        private int speed = 4;
        private NavMeshAgent agent;
        private Ghost ghost;
        private Vector3 destination;
        private bool computeDestination = true;
        private float positionThreshold = 0.01f;
        private Vector3[] pathCorners;

        public IGhostState DoState(Ghost ghost, NavMeshAgent agent, Vector3 destination)
        {
            this.ghost = ghost;
            //this.destination = destination;
            this.agent = agent;
            agent.speed = this.speed;
            ReturnHome();

            if (Mathf.Abs(ghost.transform.position.x - destination.x) < positionThreshold && Mathf.Abs(ghost.transform.position.z - destination.z) < positionThreshold)
            {
                computeDestination = true;
                return ghost.exitHomeState;
            }
            return ghost.returnHomeState;
        }

        private void ReturnHome()
        {
            //only set destination once
            if (computeDestination)
            {
                //change to return home animation (only eyes/pupils are visible)
                ChangeAnimation();

                GameObject home = GameObject.Find("Home");
                destination = new Vector3(home.transform.position.x, ghost.transform.position.y, home.transform.position.z);
                //Debug.Log("home location: " + destination);
                agent.SetDestination(destination);
                computeDestination = false;
            }
            NavMeshPath path = agent.path;
            pathCorners = path.corners;

            for (int i = 0; i < pathCorners.Length; i++)
            {
                if (Mathf.Abs(ghost.transform.position.x - pathCorners[i].x) < positionThreshold && Mathf.Abs(ghost.transform.position.z - pathCorners[i].z) < positionThreshold)
                {
                    if (i + 1 < pathCorners.Length)
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
            ghost.transform.GetChild(0).gameObject.SetActive(true);
            ghost.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            ghost.transform.GetChild(1).gameObject.SetActive(false);
        }

    }
}