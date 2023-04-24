using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Author: Aaron
namespace OttiPostLewis.Lab6
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] Vector2 targetPortalLocation;
        private Vector3 targetLocation;
        void Awake()
        {
            targetLocation = new Vector3(targetPortalLocation.x, 0, targetPortalLocation.y);
        }
        void OnTriggerEnter(Collider other)
        {
            GameObject objectToTeleport = other.gameObject;
            objectToTeleport.transform.position = new Vector3(targetLocation.x, objectToTeleport.transform.position.y, targetLocation.z);

            //do weird stuff bc modifying agent position puts it at the incorrect position 
            //Author of this block: Maddi
            NavMeshAgent agent = objectToTeleport.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                Ghost ghost = agent.GetComponent<Ghost>();
                agent.gameObject.transform.position = new Vector3(targetLocation.x, objectToTeleport.transform.position.y, targetLocation.z);
                agent.SetDestination(new Vector3(targetLocation.x, objectToTeleport.transform.position.y, targetLocation.z));
                ghost.computeInitialDest = true;
            }
        }
    }
}
