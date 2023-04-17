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

        [SerializeField] private NavMeshAgent agent;

        private IGhostState currentState;
        private float sightRange = 9f; //sight range will be as long as level's length/width but should not see through walls
        private List<Ray> sightRays = new List<Ray>();
        private Vector3 currentDirection;
        private Vector3 initialPosition;
        private Vector3 direction1;
        private Vector3 destination;
        //private Vector3 direction2;
        //private Vector3 direction3;

        private void OnEnable()
        {
            currentDirection = transform.forward;
            currentState = chaseState;
            initialPosition = transform.position;
            SetDirections();
        }

        private void SetDirections()
        {
            //3 sight rays (forward, left, right) ? 1?
            direction1 = Vector3.forward;
            //direction2 = Vector3.left;
            //direction3 = Vector3.right;

        }

        void Update()
        {
            currentState = currentState.DoState(this, currentDirection, agent, destination);
            UpdateSight();
            DrawRays();
            CheckForPacman();

            //locking x and z rotations, locking y position
            transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }

        private void UpdateSight()
        {
            sightRays.Add(new Ray(transform.position, transform.TransformDirection(direction1 * sightRange)));
            //sightRays.Add(new Ray(transform.position, transform.TransformDirection(direction2 * sightRange)));
            //sightRays.Add(new Ray(transform.position, transform.TransformDirection(direction3 * sightRange)));
        }

        private void CheckForPacman()
        {
            //Note: "Player" = pacman

            for (int i = 0; i < sightRays.Count; i++)
            {
                if (Physics.Raycast(sightRays[i], out RaycastHit hit, sightRange))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("Ghost sees pacman");
                        //set destination to pacman's current location, pass in as argument
                        currentDirection = sightRays[i].direction;
                        destination = hit.transform.position;
                        //should enter this state when sees pacman and go to last known location pacman was in
                        currentState = chaseState;
                        break;
                    }
                }
            }
            sightRays.Clear();
        }

        //for debugging purposes
        private void DrawRays()
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(direction1 * sightRange), Color.red);
            //Debug.DrawRay(transform.position, transform.TransformDirection(direction2 * sightRange), Color.red);
            //Debug.DrawRay(transform.position, transform.TransformDirection(direction3 * sightRange), Color.red);
        }

    }
}