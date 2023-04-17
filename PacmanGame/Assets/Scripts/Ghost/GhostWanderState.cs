using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VR;
using UnityEngine;
using UnityEngine.AI;

namespace OttiPostLewis.Lab6
{
    public class GhostWanderState : IGhostState
    {

        private float timer = 0;
        private float redirectTime = 5;
        private float redirectDegree = 0f;
        private int speed = 6;

        public IGhostState DoState(Ghost ghost, Vector3 direction, NavMeshAgent agent, Vector3 destination)
        {
            Wander(ghost);
            return ghost.wanderState;
        }

        private void Wander(Ghost ghost)
        {
            //randomly wander??
            ghost.transform.localPosition += ghost.transform.forward * Time.deltaTime * speed;
            timer += Time.deltaTime;

            if (timer > redirectTime)
            {
                redirectDegree = Random.Range(0.0f, 360.0f);
                ghost.transform.Rotate(0, redirectDegree, 0);
                timer = 0;
            }
        }
    }
}
