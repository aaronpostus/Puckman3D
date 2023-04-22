using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Vector3 targetPortalLocation;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        GameObject objectToTeleport = other.gameObject;
        objectToTeleport.transform.position = targetPortalLocation;
        objectToTeleport.transform.rotation = Quaternion.Euler(-transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
