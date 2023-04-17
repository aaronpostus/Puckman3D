using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VR;
using UnityEngine;

public interface IGhostState
{
    IGhostState DoState(Ghost ghost, Vector3 direction);
}
