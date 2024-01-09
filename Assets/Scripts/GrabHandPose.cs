using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

# if UNITY_EDITOR
using UnityEditor;
# endif

public class GrabHandPose : MonoBehaviour
{
    
    public HandInformation rightHandPose;
    public HandInformation leftHandPose;

    [SerializeField] private float poseTransitionDuration = 0.2f;
    private Vector3 startingHandPosition;
    private Vector3 finalHandPosition;
    private Quaternion startingHandRotation;
    private Quaternion finalHandRotation;

    private Quaternion[] startingFingerRotation;
    private Quaternion[] finalFingerRotation;
    

    private void Start()
    {
        XRGrabInteractable gravInteractable = GetComponent<XRGrabInteractable>();

        gravInteractable.selectEntered.AddListener(SetupPose);
        gravInteractable.selectExited.AddListener(UnSetPose);

        rightHandPose.gameObject.SetActive(false);
        leftHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs arg)
    {
        if(arg.interactorObject is XRDirectInteractor)
        {
            HandInformation handInformation = arg.interactorObject.transform.GetComponentInChildren<HandInformation>();
            handInformation.animator.enabled = false;

            if(handInformation.handModel == HandInformation.HandModel.Right)
            {
                SetHandInformationValues(handInformation, rightHandPose);
            }
            else
            {
                SetHandInformationValues(handInformation, leftHandPose);
            }

            
            SetHandInformation(handInformation, finalHandPosition, finalHandRotation, finalFingerRotation);
            //SetHandInformationRoutine(handInformation, finalHandPosition, finalHandRotation, finalFingerRotation, startingHandPosition, startingHandRotation, startingFingerRotation);
        }

        if (arg.interactorObject is XRRayInteractor)
        {
            HandInformation handInformation = arg.interactorObject.transform.GetComponentInParent<HandInformation>();
            handInformation.animator.enabled = false;

            if (handInformation.handModel == HandInformation.HandModel.Right)
            {
                SetHandInformationValues(handInformation, rightHandPose);
            }
            else
            {
                SetHandInformationValues(handInformation, leftHandPose);
            }


            SetHandInformation(handInformation, finalHandPosition, finalHandRotation, finalFingerRotation);
            //SetHandInformationRoutine(handInformation, finalHandPosition, finalHandRotation, finalFingerRotation, startingHandPosition, startingHandRotation, startingFingerRotation);
        }
    }

    public void UnSetPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is XRDirectInteractor)
        {
            HandInformation handInformation = arg.interactorObject.transform.GetComponentInChildren<HandInformation>();
            handInformation.animator.enabled = true;

            SetHandInformationValues(handInformation, rightHandPose);
            SetHandInformation(handInformation, startingHandPosition, startingHandRotation, startingFingerRotation);
            //SetHandInformationRoutine(handInformation,finalHandPosition, finalHandRotation, finalFingerRotation, startingHandPosition, startingHandRotation, startingFingerRotation);
        }

        if (arg.interactorObject is XRRayInteractor)
        {
            HandInformation handInformation = arg.interactorObject.transform.GetComponentInParent<HandInformation>();
            handInformation.animator.enabled = true;

            SetHandInformationValues(handInformation, rightHandPose);
            SetHandInformation(handInformation, startingHandPosition, startingHandRotation, startingFingerRotation);
            //SetHandInformationRoutine(handInformation,finalHandPosition, finalHandRotation, finalFingerRotation, startingHandPosition, startingHandRotation, startingFingerRotation);
        }
    }

    public void SetHandInformationValues(HandInformation hand1, HandInformation hand2)
    {
        startingHandPosition = hand1.root.localPosition;
        finalHandPosition = hand2.root.localPosition;

        startingHandRotation= hand1.root.localRotation;
        finalHandRotation= hand2.root.localRotation;

        startingFingerRotation = new Quaternion[hand1.fingersBones.Length];
        finalFingerRotation = new Quaternion[hand2.fingersBones.Length];

        for (int i = 0; i < hand1.fingersBones.Length; i++)
        {
            startingFingerRotation[i] = hand1.fingersBones[i].localRotation;
            finalFingerRotation[i] = hand2.fingersBones[i].localRotation;
        }
    }

    public void SetHandInformation(HandInformation hand, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation)
    {
        hand.root.localPosition = newPosition;
        hand.root.localRotation = newRotation;

        for (int i = 0; i < newBonesRotation.Length; i++)
        {
            hand.fingersBones[i].localRotation = newBonesRotation[i];
        }
    }

    public IEnumerator SetHandInformationRoutine(HandInformation hand, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation, Vector3 startingPosition, Quaternion startingRotation, Quaternion[] startingBonesRotation)
    {
        float timer = 0;

        while (timer < poseTransitionDuration)
        {
            Vector3 p = Vector3.Lerp(startingPosition, newPosition, timer/poseTransitionDuration);
            Quaternion r = Quaternion.Lerp(startingRotation, newRotation,timer/poseTransitionDuration);

            hand.root.localPosition = p;
            hand.root.localRotation = r;


            for (int i = 0; i < newBonesRotation.Length; i++)
            {
                hand.fingersBones[i].localRotation = Quaternion.Lerp(startingBonesRotation[i], newBonesRotation[i], timer / poseTransitionDuration);
            }

           timer += Time.deltaTime;
            yield return null;
        }




        hand.root.localPosition = newPosition;
        hand.root.localRotation = newRotation;

        for (int i = 0; i < newBonesRotation.Length; i++)
        {
            hand.fingersBones[i].localRotation = newBonesRotation[i];
        }
    }

# if UNITY_EDITOR

    [MenuItem("Tools/Mirror Selected Right Grab Pose")]
    public static void MirrorRightPose()
    {
        GrabHandPose handPose = Selection.activeGameObject.GetComponent<GrabHandPose>();
        handPose.MirrorPose(handPose.leftHandPose, handPose.rightHandPose);
    }
#endif
    public void MirrorPose(HandInformation poseToMirror, HandInformation poseUsedToMirror)
    {
        Vector3 mirroredPosition = poseUsedToMirror.root.localPosition;

        mirroredPosition.x *= -1f;

        Quaternion mirroredQuaternion = poseUsedToMirror.root.localRotation;
        mirroredQuaternion.y *= -1f;
        mirroredQuaternion.z *= -1f;

        poseToMirror.root.localPosition = mirroredPosition;
        poseToMirror.root.localRotation = mirroredQuaternion;

        for (int i = 0; i < poseUsedToMirror.fingersBones.Length; i++)
        {
            poseToMirror.fingersBones[i].localRotation = poseUsedToMirror.fingersBones[i].localRotation;
        }
    }
}
