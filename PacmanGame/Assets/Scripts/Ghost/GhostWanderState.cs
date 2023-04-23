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
        private bool computeInitialDest = true;
        private float positionThreshold = 0.01f;
        private float range = 100f;
        private float speed = 5f;

        public IGhostState DoState(Ghost ghost, NavMeshAgent agent, Vector3 destination)
        {
            this.ghost = ghost;
            this.agent = agent;
            agent.speed = speed * Ghost.multiplier;
            agent.updateRotation = false;
            Wander();
            
            return ghost.wanderState;
        }

        private void Wander()
        {

            if ((Mathf.Abs(ghost.transform.position.x - destination.x) < positionThreshold && Mathf.Abs(ghost.transform.position.z - destination.z) < positionThreshold) || computeInitialDest)
            {
                randomPoint = ghost.transform.position + Random.insideUnitSphere * range;
                NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, range, NavMesh.AllAreas);
                destination = hit.position;
                agent.SetDestination(destination);
            }
            computeInitialDest = false;

            NavMeshPath path = agent.path;
            GhostUtility.RotateAtCorners(ghost, path, positionThreshold);
            
        }

    }
}
