using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VR;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.GridLayoutGroup;

namespace OttiPostLewis.Lab6
{
    public class GhostWanderState : IGhostState
    {
        private Vector3 randomPoint;
        private NavMeshAgent agent;
        private Ghost ghost;
        private Vector3 destination;
        private Vector3 direction;
        private bool computeInitialDest = true;
        private float positionThreshold = 0.01f;
        private float range = 100f;
        private float speed = 3.5f;
        private Vector3[] pathCorners;

        public IGhostState DoState(Ghost ghost, NavMeshAgent agent, Vector3 destination)
        {
            this.ghost = ghost;
            this.agent = agent;
            agent.speed = speed;
            agent.updateRotation = false;
            Wander();
            
            return ghost.wanderState;
        }

        private void Wander()
        {

            if ((Mathf.Abs(ghost.transform.position.x - destination.x) < positionThreshold && Mathf.Abs(ghost.transform.position.z - destination.z) < positionThreshold) || computeInitialDest)
            {
                //Debug.Log("ghost has reached destination. choosing new destination");
                randomPoint = ghost.transform.position + Random.insideUnitSphere * range;
                NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, range, NavMesh.AllAreas);
                destination = hit.position;

                agent.SetDestination(destination);
            }
            computeInitialDest = false;

            NavMeshPath path = agent.path;
            pathCorners = path.corners;
            //Debug.Log("has path? " + agent.hasPath);
            //Debug.Log("pending? " + agent.pathPending);

            //maybe add a utility class for this??

            //loop through the path and compare each path corner to the agents current position
            for (int i = 0; i < pathCorners.Length; i++)
            {
                if (Mathf.Abs(ghost.transform.position.x - pathCorners[i].x) < positionThreshold && Mathf.Abs(ghost.transform.position.z - pathCorners[i].z) < positionThreshold)
                {
                    //Debug.Log("agent has reached next point");

                    //check which direction it should turn for the next point in the path (so it is at pathCorner i now)
                    if (i+1 < pathCorners.Length)
                    {
                        //Debug.Log("agent has reached next point");
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

    }
}
