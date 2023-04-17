using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VR;
using UnityEngine;
using UnityEngine.AI;

namespace OttiPostLewis.Lab6
{

    public interface IGhostState
    {
        IGhostState DoState(Ghost ghost, Vector3 direction, NavMeshAgent agent, Vector3 destination);
    }
}