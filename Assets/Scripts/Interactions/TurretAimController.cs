using UnityEngine;
using UnityEngine.XR;

public class TurretAimController : MonoBehaviour
{
    public Transform headTransform; // The head transform.
    public Transform turretBase;    // The base of the turret.
    public Transform turret;        // The turret object.
    public Vector2 rotationRange;

    private void Update()
    {
        float h = headTransform.localEulerAngles.y;
        float v = headTransform.localEulerAngles.x;

        //Debug.Log("h: " + h + "v: " + v);

        // Apply the horizontal rotation to the base (Y-axis rotation).
        turretBase.rotation = Quaternion.Euler(0f, h, 0f);

        turret.rotation = Quaternion.Euler(v, 0f, 0f);
    }
}
