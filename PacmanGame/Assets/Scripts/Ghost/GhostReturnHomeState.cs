using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace OttiPostLewis.Lab6
{

    public class GhostReturnHomeState : IGhostState
    {
        private int speed = 4;
        private NavMeshAgent agent;
        private Ghost ghost;
        private Vector3 destination;
        private bool computeDestination = true;
        private float positionThreshold = 0.01f;

        public IGhostState DoState(Ghost ghost, NavMeshAgent agent, Vector3 destination, bool computeInitialDest)
        {
            this.ghost = ghost;
            this.agent = agent;
            agent.speed = this.speed * Ghost.multiplier;
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
            GhostUtility.RotateAtCorners(ghost, path, positionThreshold);
        }

        private void ChangeAnimation()
        {
            ghost.transform.GetChild(0).gameObject.SetActive(true);
            ghost.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            ghost.transform.GetChild(1).gameObject.SetActive(false);
        }

    }
}