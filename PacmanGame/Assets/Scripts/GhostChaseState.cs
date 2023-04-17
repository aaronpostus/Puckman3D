using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChaseState : IGhostState
{

    private Vector3 direction;
    private int speed = 5;
    public IGhostState DoState(Ghost ghost, Vector3 direction)
    {
        this.direction = direction;
        Chase(ghost);
        //return chase until ghost location = last known location of pacman
        return ghost.wanderState;
    }

    private void Chase(Ghost ghost)
    {
        //go to last location pacman was at then go back to wander state; may need location parameter in DoState... 
        ghost.transform.rotation = Quaternion.LookRotation(direction);
        ghost.transform.localPosition += ghost.transform.forward * Time.deltaTime * speed;
    }

}
