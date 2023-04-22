using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace OttiPostLewis.Lab6
{

    public class GhostExitHomeState : IGhostState
    {

        private Vector3 direction;
        private int speed = 5;
        private NavMeshAgent agent;
        private Ghost ghost;
        private Vector3 destination;
        private bool computeDestination = true;
        private float positionThreshold = 0.01f;

        public IGhostState DoState(Ghost ghost, Vector3 direction, NavMeshAgent agent, Vector3 destination)
        {
            this.direction = direction;
            this.ghost = ghost;
            this.agent = agent;
            ExitHome();

            if (Mathf.Abs(ghost.transform.position.x - this.destination.x) < positionThreshold && Mathf.Abs(ghost.transform.position.z - this.destination.z) < positionThreshold)
            {
                computeDestination = true;
                return ghost.wanderState;
            }
            return ghost.exitHomeState;
        }

        private void ExitHome()
        {
            //change to default animation
            ChangeAnimation();

            //stagger
            //??

            if (computeDestination)
            {
                GameObject home = GameObject.Find("Home");
                destination = new Vector3(home.transform.position.x, ghost.transform.position.y, home.transform.position.z + 2.5f);
                Debug.Log("exit location: " + destination);
                agent.SetDestination(destination);
                computeDestination = false;
            }

            //do angle stuff
            //??

        }

        private void ChangeAnimation()
        {
            ghost.transform.GetChild(0).gameObject.SetActive(true);
            ghost.transform.GetChild(1).gameObject.SetActive(false);
        }

    }
}
