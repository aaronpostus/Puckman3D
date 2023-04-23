using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletPlacer : MonoBehaviour
{
    Vector3 upperLeftCorner;
    int gridLengthX, gridLengthZ;
    float lengthOfGridSpace;
    GameObject pelletPrefab;

    public PelletPlacer(Vector3 upperLeftCorner, int gridLengthX, int gridLengthZ, float lengthOfGridSpace, GameObject pelletPrefab) {
        this.upperLeftCorner = upperLeftCorner;
        this.gridLengthX = gridLengthX;
        this.gridLengthZ = gridLengthZ;
        this.lengthOfGridSpace = lengthOfGridSpace;
        this.pelletPrefab = pelletPrefab;
        for(int i = 0; i < gridLengthX; i++) {
            for(int j = 0; j < gridLengthZ; j++) {
                Vector3 pelletLocation = new Vector3(upperLeftCorner.x - (i * lengthOfGridSpace), upperLeftCorner.y, upperLeftCorner.z + (j * lengthOfGridSpace));
                Debug.Log("327");
                Debug.Log(pelletLocation);
                if(CanPlacePellet(pelletLocation)) {
                    Instantiate(pelletPrefab, pelletLocation, pelletPrefab.transform.rotation);
                }
            }
        }
    }
    bool CanPlacePellet(Vector3 location) {
        if(Physics.Raycast(location, Vector3.zero, out RaycastHit hitInfo)) {
            // clipping something
            return false;
        }
        if(Physics.Raycast(location, (-Vector3.up * 10f), out RaycastHit hitInfo2)) {
            // not clipping something and we have a floor
            return true;
        }
        // not clipping but no floor
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
