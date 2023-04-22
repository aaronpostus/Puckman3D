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

        [SerializeField] private NavMeshAgent agent;

        private IGhostState currentState;
        private float sightRange = 9f; //sight range will be as long as level's length/width but should not see through walls
        private Vector3 currentDirection;
        private Vector3 forward;
        private Vector3 destination;
        private Ray sight;

        private Vector3 initialPosition;
        //private List<Ray> sightRays = new List<Ray>();
        //private Vector3 direction2;
        //private Vector3 direction3;

        private void OnEnable()
        {
            currentDirection = transform.forward;
            forward = Vector3.forward;
            currentState = exitHomeState;
            initialPosition = transform.position;
        }

        void Update()
        {
            currentState = currentState.DoState(this, currentDirection, agent, destination);
            UpdateSight();
            DrawRay();
            CheckForPacman();

            //locking x and z rotations, locking y position ???
            //transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
            //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
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

            //sightRays.Add(new Ray(transform.position, transform.TransformDirection(forward * sightRange)));
            //sightRays.Add(new Ray(transform.position, transform.TransformDirection(direction2 * sightRange)));
            //sightRays.Add(new Ray(transform.position, transform.TransformDirection(direction3 * sightRange)));
        }

        private void CheckForPacman()
        {
            //Note: "Player" tag = pacman

            //if pacman's current state == big pacman
                //currentState = fleeState;
            
            //else if
            if (Physics.Raycast(sight, out RaycastHit hit, sightRange))
            {
                Debug.Log("ghost sees something");
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Ghost sees pacman");
                    //set destination to pacman's current location
                    destination = hit.transform.position;
                    currentState = chaseState;
                }
            }

        }

        //for debugging purposes
        private void DrawRay()
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(forward * sightRange), Color.red);
            //Debug.DrawRay(transform.position, transform.TransformDirection(direction2 * sightRange), Color.red);
            //Debug.DrawRay(transform.position, transform.TransformDirection(direction3 * sightRange), Color.red);
        }

    }
}