using UnityEngine;
using UnityEngine.AI;

//Author: Maddi
namespace OttiPostLewis.Lab6
{
    public static class GhostUtility
    {
        //manually rotates at 90 degree intervals at corners in its path
        public static void RotateAtCorners(Ghost ghost, NavMeshPath path, float positionThreshold)
        {
            Vector3 direction;
            Vector3[] pathCorners = path.corners;
            for (int i = 0; i < pathCorners.Length; i++)
            {
                if (Mathf.Abs(ghost.transform.position.x - pathCorners[i].x) < positionThreshold && Mathf.Abs(ghost.transform.position.z - pathCorners[i].z) < positionThreshold)
                {
                    //check which direction it should turn for the next point in the path (so it is at pathCorner i now)
                    if (i+1 < pathCorners.Length)
                    {
                        Debug.DrawLine(pathCorners[i], pathCorners[i+1], Color.green);
                        //find next direction ghost will travel
                        direction = (pathCorners[i+1] - pathCorners[i]).normalized;

                        if (direction.z >= 0.8f && (direction.x < direction.z))
                        {
                            //Debug.Log("rotate up");
                            ghost.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        }
                        else if (direction.x >= 0.8f && (direction.x > direction.z))
                        {
                            //Debug.Log("rotate right");
                            ghost.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                        }
                        else if (direction.x <= -0.8f && (direction.x < direction.z))
                        {
                            //Debug.Log("rotate left");
                            ghost.transform.rotation = Quaternion.Euler(0f, -90f, 0f);

                        }
                        else if (direction.z <= -0.8f)
                        {
                            //Debug.Log("rotate down");
                            ghost.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        }
                    }
                }
            }
        }
    }
}
