using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public float SurfaceY ()
    {
        return this.transform.position.y;
    }
}
