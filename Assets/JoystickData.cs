using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JoystickData : MonoBehaviour
{
    private ConfigurableJoint joint;
    public Text text1;
    public TMP_Text text2;

    private void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        if(text1 != null)
        {
            text1.text = joint.connectedBody.transform.rotation.ToString();
        }
            
        if(text2 != null)
        {
            text2.text = joint.connectedBody.transform.rotation.ToString();
        }
            
        //text1.text = getJointRotation().ToString();
    }

    public Quaternion getJointRotation()
    {
        return (Quaternion.FromToRotation(joint.axis, joint.connectedBody.transform.rotation.eulerAngles));
    }
}
