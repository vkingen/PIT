using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    public float attackRadius;
    public float normalViewAngle = 100;
    [HideInInspector] public float maxViewAngle = 360;

    [HideInInspector] public float viewAngle;

    public Transform headLocation;

    public LayerMask targets;
    public LayerMask obstacles;

    public Transform visibleTarget;
    public Transform attackTarget;



    private void Start()
    {
        viewAngle = normalViewAngle;
    }

    private void FixedUpdate()
    {
        FindTargets();
        FindAttackTargets();
    }

    private void FindTargets()
    {
        visibleTarget = null;
        float closestDistance = 1000f;
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, viewRadius, targets);
        foreach (Collider selectedTarget in targetsInRadius)
        {
            float currentDistanceToTarget = Vector3.Distance(transform.position, selectedTarget.transform.position);
            if(currentDistanceToTarget < closestDistance)
            {
                closestDistance = currentDistanceToTarget;
                Transform target = selectedTarget.transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(headLocation.position, dirToTarget, distanceToTarget, obstacles))
                        visibleTarget = target;
                }
            }
        }
    }

    private void FindAttackTargets()
    {
        attackTarget = null;
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, attackRadius, targets);
        foreach (Collider selectedTarget in targetsInRadius)
        {
            Transform target = selectedTarget.transform.Find("ProjectileTarget"); // Should be middleOfPlayer
            if (target == null) return;
        
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(headLocation.position, dirToTarget, distanceToTarget, obstacles))
                {
                    attackTarget = target;
                }

            }
        }
    }

    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
