using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Author: Maddi
namespace OttiPostLewis.Lab6
{
    public class Ghost : MonoBehaviour
    {
        public GhostWanderState wanderState = new GhostWanderState();
        public GhostChaseState chaseState = new GhostChaseState();
        public GhostFleeState fleeState = new GhostFleeState();
        public GhostReturnHomeState returnHomeState = new GhostReturnHomeState();
        public GhostExitHomeState exitHomeState = new GhostExitHomeState();
        public static bool canMove = false;
        public static float multiplier = 1;
        public bool computeInitialDest = true;
        public float initialExitTimer = 0;

        [SerializeField] private NavMeshAgent agent;

        private IGhostState currentState;
        private float sightRange = 10f; //may need to change
        private Vector3 forward;
        private Vector3 destination;
        private Ray sight;
        private Vector3 initialPosition;
        private Quaternion initialRotation;

        private void OnEnable()
        {
            forward = Vector3.forward;
            currentState = exitHomeState;
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }

        void Update()
        {
            if (canMove)
            {
                currentState = currentState.DoState(this, agent, destination, computeInitialDest);
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
        }

        private void UpdateSight()
        {         
            sight = new Ray(transform.position, transform.TransformDirection(forward * sightRange));
        }

        private void CheckForPacman()
        {
            //Note: "Player" tag = pacman

            //if pacman's current state == ghost-eating pacman, flee
            if (MovementControl.currentState == MovementControl.PacmanState.Chase && currentState != returnHomeState)
            {
                currentState = fleeState;
            }
            
            else if (Physics.Raycast(sight, out RaycastHit hit, sightRange))
            {
                //cannot enter chase state until it has left/returned home
                if (hit.collider.CompareTag("Player") && currentState != exitHomeState && currentState != returnHomeState)
                {
                    //Debug.Log("Ghost sees pacman");
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

        public void ResetGhost() {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            initialExitTimer = 0;
            currentState = exitHomeState;
        }
    }
}


//TODO:

  //adjust speeds for each level

  //test