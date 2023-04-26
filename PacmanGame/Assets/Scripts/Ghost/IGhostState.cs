using UnityEngine;
using UnityEngine.AI;

//Author: Maddi
namespace OttiPostLewis.Lab6
{

    public interface IGhostState 
    {
        IGhostState DoState(Ghost ghost, NavMeshAgent agent, Vector3 destination, bool computeInitialDest);
    }
}
