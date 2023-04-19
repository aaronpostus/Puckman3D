using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VR;
using UnityEngine;
using UnityEngine.AI;

namespace OttiPostLewis.Lab6
{
    public class GhostWanderState : IGhostState
    {
        private Vector3 randomPoint;
        private NavMeshAgent agent;
        private Ghost ghost;
        private Vector3 destination;
        private bool computeInitialDest = true;
        private float positionThreshold = 0.01f;
        private float range = 20f;
        private Vector3[] pathCorners;
        private float targetAngle;

        //private float timer = 0;
        //private float redirectTime = 5;
        //private float redirectDegree = 0f;
        //private int speed = 5;

        public IGhostState DoState(Ghost ghost, Vector3 direction, NavMeshAgent agent, Vector3 destination)
        {
            //this.direction = direction;
            this.ghost = ghost;
            this.agent = agent;
            //agent.updateRotation = false;

            //this.destination = destination;

            Wander();
            
            return ghost.wanderState;
        }

        private void Wander()
        {
            //goal: should never turn 180 degrees, should not move diagonally

            Debug.Log("ghost wandering");
            

            if ((Mathf.Abs(ghost.transform.position.x - destination.x) < positionThreshold && Mathf.Abs(ghost.transform.position.z - destination.z) < positionThreshold) || computeInitialDest)
            {
                Debug.Log("ghost has reached destination. choosing new destination");
                randomPoint = ghost.transform.position + Random.insideUnitSphere * range;
                NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, range, NavMesh.AllAreas);
                destination = hit.position;
                //Debug.Log("dest: " + destination);
                agent.SetDestination(destination);

                NavMeshPath path = agent.path;
                pathCorners = path.corners;

            }
            computeInitialDest = false;

            /*
            //loop through the path and compare each path corner to the agents current position
            for (int i = 0; i < pathCorners.Length; i++)
            {
                //if agent has reached next point
                if (Mathf.Abs(ghost.transform.position.x - pathCorners[i].x) < positionThreshold && Mathf.Abs(ghost.transform.position.z - pathCorners[i].z) < positionThreshold)
                {
                    Debug.Log("here1");
                    //check which direction it should turn for the next point in the path (so it is at pathCorner i now)
                    if (i+1 < pathCorners.Length)
                    {
                        Debug.Log("here2");
                        //if the difference in x position is greater than the difference in z position
                        if (Mathf.Abs(pathCorners[i].x - pathCorners[i + 1].x) > Mathf.Abs(pathCorners[i].z - pathCorners[i + 1].z))
                        {
                            //turn on x axis
                            Debug.Log("turning on x");
                            targetAngle = Mathf.Sign(pathCorners[i + 1].x - pathCorners[i].x) * 90f;
                            ghost.transform.rotation = Quaternion.LookRotation(new Vector3(targetAngle, 0f, 0f));
                        }
                        else
                        {
                            //turn on z axis
                            Debug.Log("turning on z");
                            targetAngle = Mathf.Sign(pathCorners[i + 1].z - pathCorners[i].z) * 90f;
                            ghost.transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, targetAngle));
                        }
                    }
                    
                    
                }
            }
            */

        }
    }
}















//when the agent reaches a waypoint, check direction it needs to turn to continue following the path:
//compare the current waypoint to the next one in the path
//if the difference in x position is greater than the difference in z position, the agent needs to turn along x axis
//otherwise, it needs to turn along the z axis

//use ghost.transform.Rotate to turn agent 90 degrees in appropriate direction
//once agent has turned, move it in a straight line towards the next waypoint using agent.SetDestination




//Debug.Log("path: " + agent.path);




//Vector3 directionToDestination = (destination - ghost.transform.position).normalized;
//Vector3 cardinalDirection = new Vector3(Mathf.Round(directionToDestination.x), 0f, Mathf.Round(directionToDestination.z)).normalized;
//ghost.transform.rotation = Quaternion.LookRotation(cardinalDirection);
//ghost.transform.Rotate(0f, -90f, 0f);
//ghost.transform.position += ghost.transform.forward * Time.deltaTime * agent.speed;







//Vector3 direction = destination - ghost.transform.position;
//direction.y = 0f;

//if absolute unsigned value of 'x' is higher that unsigned value of 'z'
//if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
//    //then we know that nav agent should turn straight to the right (on X axis)
//    //so, set the forward axis to '0'
//    direction.z = 0f;
//else
//    //otherwise make him turn straight to Z axis (set the right axis to '0')
//    direction.x = 0f;
////we've set 'direction.y' to '0' earlier so we could rotate transform to the direction
////and it will be rotated around its Y axis only.
//ghost.transform.Rotate(direction);




//Debug.Log("ghost pos x: " + ghost.transform.position.x); 
//Debug.Log("destx: " + destination.x);

//Debug.Log("ghost pos z: " + ghost.transform.position.z); 
//Debug.Log("destz: " + destination.z);
//Debug.Log((ghost.transform.position.x == destination.x));

//direction += ghost.transform.position;

//if (computeDestination)
//{
//    destination.x = Random.Range(-8.83f, 8.67f); //bad idea bc range may be different in each lvl
//    destination.z = Random.Range(-10.54f, 6.8f);
//    agent.SetDestination(new Vector3(destination.x, ghost.transform.position.y, destination.y));
//    //destination needs to be on navmesh
//}
//computeDestination = false;

//if (ghost.transform.position.x == destination.x && ghost.transform.position.z == destination.z)
//{
//    Debug.Log("ghost has reached destination. choosing new destination");
//    destination.x = Random.Range(-8.83f, 8.67f);
//    destination.z = Random.Range(-10.54f, 6.8f);
//    agent.SetDestination(new Vector3(destination.x, ghost.transform.position.z, destination.z));
//}

// Vector3 randomDirection = Random.insideUnitSphere * 8;
//randomDirection += ghost.transform.position;
//NavMeshHit hit;
//destination = Vector3.zero;
//if (NavMesh.SamplePosition(randomDirection, out hit, 8, NavMesh.AllAreas))
//{
//    destination = hit.position;
//    Debug.Log(destination);
//}
//agent.SetDestination(destination);


//always be moving
//once it reaches destination, choose new destination (or timer based?)
//x: 8.67 to -8.83
//z: -10.54 to 6.8


//ghost.transform.localPosition += ghost.transform.forward * Time.deltaTime * speed;
//timer += Time.deltaTime;

//if (timer > redirectTime)
//{
//    redirectDegree = Random.Range(0.0f, 360.0f);
//    ghost.transform.Rotate(0, redirectDegree, 0);
//    timer = 0;
//}
