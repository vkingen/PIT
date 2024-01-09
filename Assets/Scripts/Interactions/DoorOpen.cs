using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    //[SerializeField] private Vector3 targetPosition;
    //public float duration = 2f;
 

    //private Vector3 initialPosition;
    //private float startTime;

    //private void Start()
    //{
    //    if (targetPosition == null)
    //    {
    //        Debug.LogError("Target position is not assigned!");
    //        enabled = false;
    //    }
    //    initialPosition = transform.localPosition;
    //    startTime = Time.time;
    //    //StartCoroutine(MoveObject());
    //}

    public void OpenDoor()
    {
        if (!isMoving && !hasBeenActivated)
            StartCoroutine(MoveGate(targetPosition));
    }

    //private IEnumerator MoveObject()
    //{
    //    float elapsed = 0f;
    //    while (elapsed < duration)
    //    {
    //        float t = elapsed / duration;
    //        transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, t);
    //        elapsed = Time.time - startTime;
    //        yield return null;
    //    }

    //    // Ensure we end up exactly at the target position
    //    transform.localPosition = targetPosition;

    //    hasBeenActivated = true;
    //}


    private bool hasBeenActivated = false;
    public Vector3 targetPosition; // The destination position for the gate
    public float moveSpeed = 2.0f; // The speed at which the gate moves

    private Vector3 initialPosition; // The initial position of the gate
    private bool isMoving = false; // Flag to track if the gate is currently moving

    void Start()
    {
        // Store the initial position of the gate
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        //StartCoroutine(MoveGate(targetPosition));
    }

    IEnumerator MoveGate(Vector3 destination)
    {
        hasBeenActivated = true;
        isMoving = true; // Set the flag to indicate the gate is moving
        float journeyLength = Vector3.Distance(transform.localPosition, destination);
        float startTime = Time.time;

        while (transform.localPosition != destination)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.localPosition = Vector3.Lerp(initialPosition, destination, fractionOfJourney);
            yield return null;
        }

        
        isMoving = false; // Reset the flag when the movement is complete
    }
}
