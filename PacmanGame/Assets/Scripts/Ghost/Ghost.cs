using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace OttiPostLewis.Lab6
{
    public class Ghost : MonoBehaviour
    {
        public GhostWanderState wanderState = new GhostWanderState();
        public GhostChaseState chaseState = new GhostChaseState();
        public GhostFleeState fleeState = new GhostFleeState();
        public GhostReturnHomeState returnHomeState = new GhostReturnHomeState();
        public GhostExitHomeState exitHomeState = new GhostExitHomeState();
        public static bool canMove = true;
        public static float multiplier = 1;

        [SerializeField] private NavMeshAgent agent;

        private IGhostState currentState;
        private float sightRange = 10f; //may need to change
        private Vector3 forward;
        private Vector3 destination;
        private Ray sight;

        private void OnEnable()
        {
            forward = Vector3.forward;
            currentState = exitHomeState;
            //Physics.IgnoreCollision(GetComponent<Collider>(), otherObject.GetComponent<Collider>());
        }

        void Update()
        {
            if (canMove)
            {
                currentState = currentState.DoState(this, agent, destination);
                UpdateSight();
                DrawRay();
                CheckForPacman();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //if in flee state and collides with pacman, return home
            if (currentState == fleeState && other.gameObject.CompareTag("Player"))
            {
                currentState = returnHomeState;
            }
            Debug.Log("hitting something");
        }

        private void UpdateSight()
        {         
            sight = new Ray(transform.position, transform.TransformDirection(forward * sightRange));
        }

        private void CheckForPacman()
        {
            //Note: "Player" tag = pacman

            //if pacman's current state == big pacman
                //currentState = fleeState;
            
            //else if
            if (Physics.Raycast(sight, out RaycastHit hit, sightRange))
            {
                //Debug.Log("ghost sees something");
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Ghost sees pacman");
                    //set destination to pacman's current location
                    destination = hit.transform.position;
                    currentState = chaseState;
                }
            }

        }

        private void DrawRay()
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(forward * sightRange), Color.red);
        }

    }
}


//TODO:

  //add pacman state checks 

  //adjust speeds for each level

  //ghost reset

  //test