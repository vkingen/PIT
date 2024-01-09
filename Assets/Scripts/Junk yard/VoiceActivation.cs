using Meta.WitAi;
using Meta.WitAi.Requests;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Oculus.VoiceSDK.UX
{

    public class VoiceActivation : MonoBehaviour
    {
        [Tooltip("Reference to the current voice service")]
        [SerializeField] private VoiceService _voiceService;

        [Tooltip("Text to be shown while the voice service is not active")]
        [SerializeField] private string _activateText = "Activate";
        [Tooltip("Whether to immediately send data to service or to wait for the audio threshold")]
        [SerializeField] private bool _activateImmediately = false;

        [Tooltip("Text to be shown while the voice service is active")]
        [SerializeField] private string _deactivateText = "Deactivate";
        [Tooltip("Whether to immediately abort request activation on deactivate")]
        [SerializeField] private bool _deactivateAndAbort = false;



        // Current request
        private VoiceServiceRequest _request;
        private bool _isActive = false;
        private bool activationToggle = false;
        private bool isCorrectAngle = false;

        private bool methodExecuted = false;
        

        [Header("Inputs")]
        public InputActionProperty voiceActivationButton;

        // Get button & label
        private void Awake()
        {
            //_button = GetComponent<Button>();
            if (_voiceService == null)
            {
                _voiceService = FindObjectOfType<VoiceService>();
            }
        }

        private void Update()
        {
            //Shoot();
            if (voiceActivationButton.action.ReadValue<float>() > 0 || Input.GetKey(KeyCode.J))
            {
                isCorrectAngle = true;
            }
            else
            {
                isCorrectAngle = false;
                activationToggle = false;
                return;
            }

            if (isCorrectAngle)
            {
                if (!activationToggle)
                {
                    activationToggle = true;
                    OnClick();
                }
            }






            //if (voiceActivationButton.action.ReadValue<float>() > 0 || Input.GetKey(KeyCode.J))
            //{
                

            //    if (!activationToggle)
            //    {
            //        activationToggle = true;
            //        OnClick();
            //    }
            //}
            //else
            //{
            //    _isActive = false;
            //    activationToggle = false;
            //    OnClick();

            //}



            ////if (methodExecuted) return;
            //float currentZRotation = targetTransform.eulerAngles.z;
            ////Debug.Log(targetTransform.eulerAngles.z);
            //if (currentZRotation >= minAngle && currentZRotation <= maxAngle)
            //{
            //    isCorrectAngle = true;
            //}
            //else
            //{
            //    isCorrectAngle = false;
            //    activationToggle = false;
            //    meshRenderer.material = OFF;
            //    return;
            //}

            //if(isCorrectAngle)
            //{
            //    if (!activationToggle)
            //    {
            //        activationToggle = true;
            //        meshRenderer.material = ON;
            //        OnClick();
            //    }
            //}
        }

        //private void CheckDirection()
        //{
        //    if (handTransform != null)
        //    {
        //        Quaternion handRotation = handTransform.rotation;
        //        Vector3 upwardDirection = handRotation * Vector3.up;
        //        float angle = Vector3.Angle(upwardDirection, Vector3.up);


        //        if (angle < threshold)
        //        {
        //            //Debug.Log("Palm is facing upwards");
        //            isCorrectAngle = true;
        //        }
        //        else
        //        {
        //            //Debug.Log("Palm is not facing upwards");
        //            isCorrectAngle = false;
        //            activationToggle = false;
        //            meshRenderer.material = OFF;
        //            return;
        //        }
        //    }
        //    if (isCorrectAngle)
        //    {
        //        if (!activationToggle)
        //        {
        //            activationToggle = true;
        //            meshRenderer.material = ON;
        //            OnClick();
        //        }
        //    }
        //}

        // Add click delegate
        private void OnEnable()
        {
            RefreshActive();
            //if (_button != null)
            //{
            //    _button.onClick.AddListener(OnClick);
            //}
        }
        // Remove click delegate
        private void OnDisable()
        {
            _isActive = false;
            //if (_button != null)
            //{
            //    _button.onClick.RemoveListener(OnClick);
            //}
        }

        // On click, activate if not active & deactivate if active
        private void OnClick()
        {
            if (!_isActive)
            {
                Debug.Log("Activate");
                Activate();
            }
            else
            {
                Debug.Log("Deactivate");
                Deactivate();
            }
        }

        // Activate depending on settings
        private void Activate()
        {
            //Debug.Log("Activate");
            if (!_activateImmediately)
            {
                _request = _voiceService.Activate(GetRequestEvents());
            }
            else
            {
                _request = _voiceService.ActivateImmediately(GetRequestEvents());
            }
        }

        // Deactivate depending on settings
        private void Deactivate()
        {
            //Debug.Log("Deactivate");
            if (!_deactivateAndAbort)
            {
                _request.DeactivateAudio();
            }
            else
            {
                _request.Cancel();
            }
        }

        // Get events
        private VoiceServiceRequestEvents GetRequestEvents()
        {
            VoiceServiceRequestEvents events = new VoiceServiceRequestEvents();
            events.OnInit.AddListener(OnInit);
            events.OnComplete.AddListener(OnComplete);
            return events;
        }
        // Request initialized
        private void OnInit(VoiceServiceRequest request)
        {
            _isActive = true;
            RefreshActive();
        }
        // Request completed
        private void OnComplete(VoiceServiceRequest request)
        {
            _isActive = false;
            RefreshActive();
        }

        // Refresh active text
        private void RefreshActive()
        {
            //Debug.Log(_isActive ? _deactivateText : _activateText);


            //Debug.Log(_isActive);
        }
    }
}
