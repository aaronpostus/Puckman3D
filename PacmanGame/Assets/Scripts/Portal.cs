using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Vector2 targetPortalLocation;
    private Vector3 targetLocation;
    void Awake() {
        targetLocation = new Vector3(targetPortalLocation.x, 0, targetPortalLocation.y);
    }
    void OnTriggerEnter(Collider other)
    {
        GameObject objectToTeleport = other.gameObject;
        objectToTeleport.transform.position = new Vector3(targetLocation.x, objectToTeleport.transform.position.y, targetLocation.z);
        objectToTeleport.transform.rotation = Quaternion.Euler(-transform.forward);
    }
}
