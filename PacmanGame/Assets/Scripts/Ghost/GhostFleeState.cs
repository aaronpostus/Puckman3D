using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace OttiPostLewis.Lab6
{

    public class GhostFleeState : IGhostState
    {

        private Vector3 direction;
        private int speed = 5; //speed should be less than pacman's
        private float timer = 0;
        [SerializeField] float fleeTime = 6;

        public IGhostState DoState(Ghost ghost, Vector3 direction, NavMeshAgent agent, Vector3 destination)
        {
            this.direction = direction;
            Flee(ghost);
            timer += Time.deltaTime;

            if (timer > fleeTime)
            {
                timer = 0;
                return ghost.wanderState;
            }
            return ghost.fleeState;
        }

        private void Flee(Ghost ghost)
        {
            //enable ghost flee objects and disable others
            //use navMesh to move as far from pacman's position as possible
            //ghost.transform.rotation = Quaternion.LookRotation(direction);
            //ghost.transform.localPosition += ghost.transform.forward * Time.deltaTime * speed;
        }

        //goal is to enable whatever is disabled and vice versa
        private void ChangeAnimation()
        {

        }

    }
}
