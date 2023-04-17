using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VR;
using UnityEngine;

public class PredatorWanderState : IGhostState
{

    private float timer = 0;
    private float redirectTime = 5;
    private float redirectDegree = 0f;
    private int speed = 6;

    public IGhostState DoState(Ghost ghost, Vector3 direction)
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

