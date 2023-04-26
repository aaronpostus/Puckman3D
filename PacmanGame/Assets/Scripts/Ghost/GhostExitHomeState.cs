using UnityEngine;
using UnityEngine.AI;

//Author: Maddi
namespace OttiPostLewis.Lab6
{

    public class GhostExitHomeState : IGhostState
    {
        private NavMeshAgent agent;
        private Ghost ghost;
        private Vector3 destination;
        private bool computeDestination = true;
        private float positionThreshold = 0.01f;
        private float homeZOffset = 2.5f;
        private float clydeLeaveTime = 3f;
        private float pinkyLeaveTime = 6f;
        private float inkyLeaveTime = 9f;

        public IGhostState DoState(Ghost ghost, NavMeshAgent agent, Vector3 destination, bool computeInitialDest)
        {
            this.ghost = ghost;
            this.agent = agent;
            ExitHome();

            if (Mathf.Abs(ghost.transform.position.x - this.destination.x) < positionThreshold && Mathf.Abs(ghost.transform.position.z - this.destination.z) < positionThreshold)
            {
                computeDestination = true;
                ghost.computeInitialDest = true;
                return ghost.wanderState;
            }
            return ghost.exitHomeState;
        }

        private void ExitHome()
        {
            ghost.initialExitTimer += Time.deltaTime;

            if (computeDestination)
            {
                //change to default animation
                ChangeAnimation();
                GameObject home = GameObject.Find("Home");
                destination = new Vector3(home.transform.position.x, ghost.transform.position.y, home.transform.position.z + homeZOffset);
                Stagger();
            }

        }

        private void ChangeAnimation()
        {
            ghost.transform.GetChild(0).gameObject.SetActive(true);
            ghost.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            ghost.transform.GetChild(1).gameObject.SetActive(false);
        }

        //only staggers at the start of the level so timer is not reset (unless level is reset)
        private void Stagger()
        {
            if (ghost.name == "Blinky")
            {
                agent.SetDestination(destination);
                computeDestination = false;
            }

            else if (ghost.name == "Clyde" && ghost.initialExitTimer > clydeLeaveTime)
            {
                agent.SetDestination(destination);
                computeDestination = false;
            }

            else if (ghost.name == "Pinky" && ghost.initialExitTimer > pinkyLeaveTime)
            {
                agent.SetDestination(destination);
                computeDestination = false;
            }

            else if (ghost.name == "Inky" && ghost.initialExitTimer > inkyLeaveTime)
            {
                agent.SetDestination(destination);
                computeDestination = false;
            }
        }

    }
}
