using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public void SetGravity(float gravity)
    {
        Physics.gravity = new Vector3(0, gravity, 0);
    }
}
