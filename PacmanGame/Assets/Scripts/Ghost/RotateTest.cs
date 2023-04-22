using OttiPostLewis.Lab6;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RotateTest : MonoBehaviour
{
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;


        //Vector3 direction = pathCorners[i + 1] - pathCorners[i];

        if (timer > 3 && timer < 6)
        {
            Debug.Log("facing up");
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (timer > 6 && timer < 9)
        {
            Debug.Log("facing right");
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else if (timer > 9 && timer < 12)
        {
            Debug.Log("facing left");
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
        else if (timer > 12 && timer < 15)
        {
            Debug.Log("facing down");
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            timer = 0;
        }

    }
}
