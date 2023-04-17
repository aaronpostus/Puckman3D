using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFleeState : IGhostState
{

    private Vector3 direction;
    private int speed = 5; //speed should be less than pacman's
    private float timer = 0;
    [SerializeField] float fleeTime = 6;

    public IGhostState DoState(Ghost ghost, Vector3 direction)
    {
        this.direction = direction;
        Flee(ghost);
        timer += Time.deltaTime;

        if (timer > fleeTime)
        {
            timer = 0;
            return ghost.wanderState;
        }
        return ghost.fleeState;
    }

    private void Flee(Ghost ghost)
    {
        //use navMesh to move as far from pacman's position as possible
        ghost.transform.rotation = Quaternion.LookRotation(direction);
        ghost.transform.localPosition += ghost.transform.forward * Time.deltaTime * speed;
    }

}
