using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour
{
    public bool pcTesting = false;

    public float throttleIncrement = 0.1f;
    public float maxThrottle = 200f;
    public float responsiveness = 10f;
    public float rollModifier = 1.3f;

    [HideInInspector] public float throttleValue;
    private float rollValue;
    private float pitchValue;
    private float yawValue;
    private float throttle;
    private float brake;

    [SerializeField] private bool engineIsOn = true;

    public float liftValue = 135f;

    //public float stabilizationRate = 0.1f; // Adjust the rate as needed.

    [HideInInspector] public Rigidbody rigidbody;

    public InputActionProperty shipMovementInput;
    public InputActionProperty shipOrientationInput;
    
    public InputActionProperty throttleInput;
    public InputActionProperty brakeInput;

    public TMP_Text throttleText, speedText;
    public Slider throttleSlider;

    public AudioSource throttleAudioSource;
    public float minVelocity = 0f;  // Minimum velocity in km/h
    public float maxVelocity = 140f;  // Maximum velocity in km/h
    public float minPitch = 0.8f;  // Minimum pitch
    public float maxPitch = 1.2f;  // Maximum pitch

    private float startingThrottleVolume;

    private float responseModifier
    {
        get
        {
            return (rigidbody.mass / 10f) * responsiveness;
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        throttleAudioSource = GetComponent<AudioSource>();
        startingThrottleVolume = throttleAudioSource.volume;
    }

    public void StartEngine()
    {
        engineIsOn = true;
        // Play start engine sounds (Booting up)
        // Enable UI elements; (Maybe with flicker effect)
    }

    private void UpdateUI()
    {
        throttleText.text = "THROTTLE: " + throttleValue.ToString("F0") + "%";
        throttleSlider.value = throttleValue / 100;
        speedText.text = "VELOCITY: " + (rigidbody.velocity.magnitude * 3.6f).ToString("F0") + "KM/H";
    }

    private void HandleThrottleAudio(bool engineIsOn)
    {
        if(engineIsOn)
        {
            float velocityKMH = rigidbody.velocity.magnitude * 3.6f;
            float normalizedVelocity = (velocityKMH - minVelocity) / (maxVelocity - minVelocity);
            float pitch = Mathf.Lerp(minPitch, maxPitch, normalizedVelocity);

            throttleAudioSource.volume = startingThrottleVolume; // Not nessesary to run this in update. Not very optimized. 
            throttleAudioSource.pitch = pitch;
        }
        else
        {
            throttleAudioSource.volume = 0f; // Not nessesary to run this in update. Not very optimized. 
        }
        
    }

    private void HandleInput()
    {
        if (!engineIsOn) return;


        if (pcTesting)
        {
            yawValue = Input.GetAxis("Yaw");
            rollValue = Input.GetAxis("Roll");
            pitchValue = Input.GetAxis("Pitch");
        }
        else
        {
            yawValue = shipMovementInput.action.ReadValue<Vector2>().x;
            rollValue = shipOrientationInput.action.ReadValue<Vector2>().x;
            pitchValue = shipOrientationInput.action.ReadValue<Vector2>().y;
        }

        if (pcTesting)
        {
            bool tempThrottle = Input.GetButton("Jump");
            bool tempBrake = Input.GetButton("Brake");

            if (tempThrottle == true)
            {
                throttleValue += throttleIncrement;
            }
            if (tempBrake == true)
            {
                throttleValue -= throttleIncrement;
            }
        }
        else
        {
            throttle = throttleInput.action.ReadValue<float>();
            brake = brakeInput.action.ReadValue<float>();
        }
        
        if (throttle >= 1) { throttleValue += throttleIncrement; }
        if (brake >= 1) { throttleValue -= throttleIncrement; }
        
        throttleValue = Mathf.Clamp(throttleValue, 0f, 100f);
    }


    private void Update()
    {
        HandleInput();
        if(engineIsOn)
            HandleThrottleAudio(true);
        else
            HandleThrottleAudio(false);
    }

    private void FixedUpdate()
    {
        if (!engineIsOn) return;
        UpdateUI();

        rigidbody.AddForce(transform.forward * maxThrottle * throttleValue);
        
        rigidbody.AddTorque(transform.up * yawValue *  responseModifier);
        rigidbody.AddTorque(transform.right * pitchValue * responseModifier);
        rigidbody.AddTorque(-transform.forward * rollValue * responseModifier * rollModifier);
    }
    public bool isColliding = false;
    private void OnCollisionEnter(Collision other)
    {
        isColliding = true;
    }
    private void OnCollisionExit(Collision other)
    {
        isColliding = false;
    }
}
