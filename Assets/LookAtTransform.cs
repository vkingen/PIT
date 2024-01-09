using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTransform : MonoBehaviour
{
    [SerializeField] private Transform lookAtTarget;
    private void FixedUpdate()
    {
        if(lookAtTarget != null)
            transform.LookAt(lookAtTarget);
    }
}
